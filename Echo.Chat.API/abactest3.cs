namespace Echo.Chat.API.AbacTest;

using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

// --- Models (same as before, omitted for brevity) ---

// --- DbContext (same as before) ---

// --- AuthorizeAction Attribute (updated) ---

/// <summary>
/// NOTE: Perhaps this should be extended to support multiple functions which could share perms and requirements, 
/// but for now each function having a uniquely specified set of requirements and permissions is okay.
/// </summary>
/// <remarks>
/// Descriptor string which is displayed to the admin, specifying what the function does, 
/// allowing them to reflect upon that when making permissions.
/// </remarks>
[AttributeUsage(AttributeTargets.Method)]
public class AuthorizeActionAttribute : Attribute
{
    /// <summary>
    /// Actual primary key, can be any string, best kept unique and constant. //maybe change to guid like tenancy systems
    /// </summary>
    public string Id { get; }
    /// <summary>
    /// Meant to be little more volatile debuggable, nameof consumer service class e.g. "CategoryService".
    /// </summary>
    public string ServiceName { get; }
    /// <summary>
    /// Meant to be little more volatile debuggable, nameof consumer function e.g. "EditCategory"
    /// </summary>
    public string ActionName { get; }
    /// <summary>
    /// Description... idk if this should be supported via the attribute, 
    /// maybe this should only be accessible to set in <see cref="RegisteredAction"/> after entity creation.
    /// </summary>
    public string? Description { get; }

    public AuthorizeActionAttribute(string id, string serviceName, string actionName)
    {
        Id = id;
        ServiceName = serviceName;
        ActionName = actionName;
    }
    public AuthorizeActionAttribute(string id, string serviceName, string actionName, string? description)
    {
        Id = id;
        ServiceName = serviceName;
        ActionName = actionName;
        Description = description;
    }
}

/// <summary>
///  If you want to make a new ID key for a new action which could be managed you can do it here.
///  The key must only be used once otherwise multiple service actions will have shared permissions and requirements.
///  NOTE: If the key constant value is changed then the action will be orphaned.
/// </summary>
public static class DomainServiceActionConstants
{
    public static string Category_Service_Edit_Category = nameof(Category_Service_Edit_Category);
}



// --- RegisteredAction Model (updated for new key) ---

public class RegisteredAction : BaseEntity<string>
{
    public string ServiceName { get; set; } = default!; // e.g. "CategoryService"
    public string ActionName { get; set; } = default!; // e.g. "EditCategory"
    //public string? ResourceType { get; set; }
    public string? Description { get; set; }
    /// <summary>
    /// Can be any string value, these <see cref="RequiredPermissions"/> are validated and compared to a <see cref="Policy.PermissionGrants"/>'s, 
    /// that is returned when the policy is fulfilled.
    /// </summary>
    public List<RegisteredActionRequiredPermission> RequiredPermissions { get; set; } = new();
}

/// <summary>
/// this is basically a specified unique or reused permission which can be then easily linked through between registeredactions and policyactions
/// </summary>
public class Permission : BaseEntity<Guid>
{

    /// <summary>
    /// should be unique e.g "CPR.Download", "User_Edit", "Wipe Prod DB", "read gdpr", etc... <- nameing doesnt really matter as it is mapped via required and granted scopes,
    /// though we should try to be consistent, so they dont use mismatched permissions. (Personally i like underscore or space as word seperators)
    /// </summary>
    public string Name { get; set; } = default!;

    public List<RegisteredActionRequiredPermission> RequiredPermissions { get; set; } = new();
    public List<PolicyPermissionGrant> PolicyPermissionGrants { get; set; } = new();
}

/// <summary>
/// this is used explicitly to map <see cref="Permission"/> to a <see cref="RegisteredAction"/>
/// </summary>
public class RegisteredActionRequiredPermission //this needs a marker and another implementation of ensureauthorize as multikey entities are currently not supported. //this naming is to prevent future naming collisions if we want to make required permissions for something else.
{
    //public Guid Id { get; set; } //probably dont need its own id as the uniqueness of the registered action contra the permission is a combined identity for this entity as it is basically a jointable.
    public Guid RegisteredActionId { get; set; }
    public Guid PermissionId { get; set; }
    public RegisteredAction? RegisteredAction { get; set; }
    public Permission? Permission { get; set; }
}

// --- Startup reflection scanner for service methods ---

public static class ServiceActionRegistration
{
    public static async Task RegisterServiceActionsAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AbacDbContext>();

        var serviceTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            //this where statement can be skipped if we want to have an entire assembly scan,
            //but for now we should just scan for services, which can be infered by the name,
            //or we could also have tagged it with an empty interface marker.
            .Where(t => t.IsClass && t.Name.EndsWith("Service"));

        foreach (var serviceType in serviceTypes)
        {
            var methods = serviceType.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(m => m.GetCustomAttribute<AuthorizeActionAttribute>() != null);

            foreach (var method in methods)
            {
                var attr = method.GetCustomAttribute<AuthorizeActionAttribute>()!;

                var id = attr.Id;
                var actionName = attr.ActionName;
                var serviceName = attr.ServiceName;
                var description = attr.Description;

                if (!db.RegisteredActions.Any(a => a.Id == id))
                {
                    db.RegisteredActions.Add(new RegisteredAction
                    {
                        Id = id,
                        ActionName = actionName,
                        ServiceName = serviceName,
                        Description = description, //this feels dirty...
                    });
                }
            }
        }

        await db.SaveChangesAsync();
    }
}

// --- ABAC Authorization Service ---

public class AbacAuthorizationService
{
    private readonly AbacDbContext _db;
    private Dictionary<string, object>? _cachedSubject;
    public AbacAuthorizationService(AbacDbContext db)
    {
        _db = db;
    }

    public async Task<bool> EnsureAuthorizedAsync<TSubject, TResource>(TSubject subject, TResource? resource, string id, string serviceName, string actionName)
        where TSubject : class, IEntity, new()
        where TResource : class, IEntity, new()
    {
        //this fetches the admin specified requirements for the function.
        var registered = await _db.RegisteredActions
            .Include(a => a.RequiredPermissions)
            .FirstOrDefaultAsync(a => a.Id == id);

        //this is to prevent usage from unregistered actions and also if an action has been registered but admins have yet to specify auth rules. (maybe the tag count check should just be let through?)
        if (registered == null || registered.RequiredPermissions.Count == 0)
            //throw new UnauthorizedAccessException("Action is not registered or has no tags.");
            return false;

        var subjectType = typeof(TSubject).Name; //this logic will break if anyone is stupid and makes two domain types names the exact same thing even if they exist in different folders. (maybe use assembly full name instead)
        var resourceType = typeof(TResource).Name; //this logic will break if anyone is stupid and makes two domain types names the exact same thing even if they exist in different folders. (maybe use assembly full name instead)

        //this fetches the policies that has been specified for this type of subject -> resource interaction e.g. user -> category
        var policies = await _db.Policies
            .Include(p => p.PermissionGrants)
            .Include(p => p.Conditions)
            .Where(p => p.SubjectType == subjectType && p.ResourceType == resourceType && p.PermissionGrants.Any(a => registered.RequiredPermissions.Any(rp => rp.PermissionId == a.PermissionId))) //could be all instead of any if policies cannot give partial access
            .ToListAsync();

        //if (!_cachedSubject.TryGetValue(subject.Id.ToString(), out object val))
        //{
        //    var subject = await GetSubjectAttributesAsync();

        //}

        //these are used to build an includeQuery if the properties are not present in the subject.
        //(this method could probably be extended or have a helper function to allow the user to fetch before auth with correct properties even if they dont know them)
        var requiredSubjectAttributes = policies.SelectMany(e => e.Conditions)
            .DistinctBy(e => e.SubjectAttribute)
            .Where(e => !string.IsNullOrEmpty(e.SubjectAttribute))
            .Select(e => e.SubjectAttribute!); //these will be relative paths e.g. Address.Country.Name (making the full tree = User.Address.Country.Name) 
        var requiredResourceAttributes = policies.SelectMany(e => e.Conditions)
            .DistinctBy(e => e.ResourceAttribute)
            .Where(e => !string.IsNullOrEmpty(e.ResourceAttribute))
            .Select(e => e.ResourceAttribute!); //these will be relative paths e.g. Category.Name (making the full tree = Category.Name)

        IQueryable<TSubject> subjectQuery = _db.Set<TSubject>().AsQueryable();

        subjectQuery = BuildIncludeQueryFromRequiredAttributes(subjectQuery, requiredSubjectAttributes);

        //not tracked as we dont want to expose the consumer to unwanted data, or make them reliant on includes via this function as that is misuse.
        TSubject? internalSubject = await subjectQuery.AsNoTracking().FirstOrDefaultAsync(e => e.Id.ToString() == subject.Id.ToString());
        TResource? internalResource = null;

        if (resource != null)
        {
            IQueryable<TResource> resourceQuery = _db.Set<TResource>().AsQueryable();

            resourceQuery = BuildIncludeQueryFromRequiredAttributes(resourceQuery, requiredResourceAttributes);
            //not tracked as we dont want to expose the consumer to unwanted data, or make them reliant on includes via this function as that is misuse.
            internalResource = await resourceQuery.AsNoTracking().FirstOrDefaultAsync(e => e.Id.ToString() == resource.Id.ToString());
        }

        var allowed = policies.Any(p =>
        {
            var matches = p.Conditions.Select(cond =>
            {
                string? left = cond.SubjectAttribute != null ? GetValueOrDefault(internalSubject!, cond.SubjectAttribute)?.ToString()
                            : cond.ResourceAttribute != null ? GetValueOrDefault(internalResource, cond.ResourceAttribute)?.ToString()
                            : null;
                return cond.Operator.ToLower() switch
                {
                    "equals" => left == cond.Value,
                    "notequals" => left != cond.Value,
                    "greaterthan" => Compare(left, cond.Value) > 0,
                    "greaterthanorequal" => Compare(left, cond.Value) >= 0,
                    "lessthan" => Compare(left, cond.Value) < 0,
                    "lessthanorequal" => Compare(left, cond.Value) <= 0,
                    //dont know if these makes sense for abac.
                    //"in" => cond.Value is IEnumerable list && list.Cast<object>().Contains(left),
                    //"notin" => cond.Value is IEnumerable nlist && !nlist.Cast<object>().Contains(left),
                    //"contains" => left is string lstr && cond.Value is string rstr && lstr.Contains(rstr, StringComparison.OrdinalIgnoreCase),
                    _ => false
                };
            });
            return p.RequireAllConditions ? matches.All(x => x) : matches.Any(x => x);
        });

        //if (!allowed) throw new UnauthorizedAccessException("Access denied by policy.");
        return false;
    }

    /// <summary>
    /// Takes a list of string property navigation paths e.g Item.Details.CreatedAt and builds an include statement for each property.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="requiredAttributes"></param>
    /// <param name="autoSplitQuery"></param>
    /// <returns></returns>
    private static IQueryable<TEntity> BuildIncludeQueryFromRequiredAttributes<TEntity>(IQueryable<TEntity> query, IEnumerable<string> requiredAttributes, bool autoSplitQuery = true) where TEntity : class, IEntity, new()
    {
        foreach (var requiredAttribute in requiredAttributes)
        {
            //if the property names have been changed or is stored wrongly the includes will fail so therefore it is neccessary to reflect changes in the db aswell
            query = query.Include(requiredAttribute);
        }

        //to account for unknowing people, accidentially making cartesian explosions.
        if (autoSplitQuery)
        {
            query = query.AsSplitQuery();
        }

        return query;

    }

    private static int Compare(object? a, object? b)
    {
        if (a == null || b == null) return 0;
        return Comparer.DefaultInvariant.Compare(a, b);
    }

    public static object? GetValueOrDefault(object? obj, string path)
    {
        if (obj == null || string.IsNullOrEmpty(path))
            return null;

        var parts = path.Split('.');
        object? currentObject = obj;

        foreach (var part in parts)
        {
            if (currentObject == null)
                return null;

            var type = currentObject.GetType();

            // Try to get property
            var prop = type.GetProperty(part, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null)
            {
                currentObject = prop.GetValue(currentObject);
                continue;
            }

            // If no property, try to get field
            var field = type.GetField(part, BindingFlags.Public | BindingFlags.Instance);
            if (field != null)
            {
                currentObject = field.GetValue(currentObject);
                continue;
            }

            // Property or field not found
            return null;
        }

        return currentObject;
    }

    private static bool IsValidPath<T>(string path)
    {
        Type currentType = typeof(T);

        foreach (var segment in path.Split('.'))
        {
            var prop = currentType.GetProperty(segment, BindingFlags.Public | BindingFlags.Instance);
            if (prop == null) return false;

            currentType = prop.PropertyType;
        }

        return true;
    }
}

// --- Example CategoryService ---

public class CategoryService
{
    private readonly AbacAuthorizationService _abac;

    public CategoryService(AbacAuthorizationService abac)
    {
        _abac = abac;
    }

    [AuthorizeAction(nameof(DomainServiceActionConstants.Category_Service_Edit_Category), nameof(CategoryService), nameof(EditCategoryAsync))]
    public async Task EditCategoryAsync(Guid categoryId)
    {
        // Resource attributes example:
        var resourceAttributes = new Dictionary<string, object>
        {
            ["CategoryLevel"] = "top",
            ["Type"] = "category"
        };

        await _abac.EnsureAuthorizedAsync(new User(), new User(), "Category_Service_Edit_Category", nameof(CategoryService), nameof(EditCategoryAsync));

        // Actual edit logic here (DB updates etc.)
    }
}

// --- Models ---

public class User : BaseEntity<Guid>
{
    public string Role { get; set; } = default!;
    public bool IsAdmin { get; set; }
    public string Department { get; set; } = default!;
}

public class Policy
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string SubjectType { get; set; } = default!;
    public string ResourceType { get; set; } = default!;
    public bool RequireAllConditions { get; set; } = true;
    public List<PolicyPermissionGrant> PermissionGrants { get; set; } = new();
    public List<PolicyCondition> Conditions { get; set; } = new();
}

public class PolicyPermissionGrant
{
    public Guid Id { get; set; }
    public Guid PolicyId { get; set; }
    public Guid PermissionId { get; set; }
    public Permission? Permission { get; set; }
}

public class PolicyCondition
{
    public Guid Id { get; set; }
    public Guid PolicyId { get; set; }
    public string? SubjectAttribute { get; set; }
    public string? ResourceAttribute { get; set; }
    public string Operator { get; set; } = default!;
    public string? Value { get; set; }
}

// --- DbContext ---

public class AbacDbContext : DbContext
{
    public AbacDbContext(DbContextOptions<AbacDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Permission> Permissions { get; set; } = null!;
    public DbSet<Policy> Policies { get; set; } = null!;
    public DbSet<PolicyPermissionGrant> PolicyActions { get; set; } = null!;
    public DbSet<PolicyCondition> PolicyConditions { get; set; } = null!;
    public DbSet<RegisteredAction> RegisteredActions { get; set; } = null!;
    public DbSet<RegisteredActionRequiredPermission> RegisteredActionRequiredPermissions { get; set; } = null!;
}

// --- Seed Data ---

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AbacDbContext>();

        if (await db.Users.AnyAsync()) return;

        var users = new List<User>
        {
            new User { Id = Guid.NewGuid(), Role = "HR", IsAdmin = false, Department = "Human Resources" },
            new User { Id = Guid.NewGuid(), Role = "SeniorHR", IsAdmin = false, Department = "Human Resources" },
            new User { Id = Guid.NewGuid(), Role = "Admin", IsAdmin = true, Department = "IT" },
        };

        db.Users.AddRange(users);
        await db.SaveChangesAsync();
    }
}

// --- AuthorizeActionAttribute --- //this way could probably be used to start authorizing async in the background while the method runs and wait for authorized before commit.

//[AttributeUsage(AttributeTargets.Method)]
//public class AuthorizeActionAttribute : Attribute, IAsyncAuthorizationFilter
//{
//    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
//    {
//        var path = context.HttpContext.Request.Path.ToString().ToLower();
//        var method = context.HttpContext.Request.Method;

//        var db = context.HttpContext.RequestServices.GetRequiredService<AbacDbContext>();
//        var abacService = context.HttpContext.RequestServices.GetRequiredService<AbacAuthorizationService>();

//        var registered = await db.RegisteredActions.FirstOrDefaultAsync(a => a.Route == path && a.HttpMethod == method);

//        if (registered is null || !registered.Tags.Any())
//        {
//            context.Result = new ForbidResult();
//            return;
//        }

//        var resourceAttributes = new Dictionary<string, object>
//        {
//            ["Type"] = registered.ResourceType ?? ""
//        };

//        var authorized = await abacService.IsAuthorizedAsync(registered.Tags.First(), registered.ResourceType ?? "", resourceAttributes);

//        if (!authorized)
//        {
//            context.Result = new ForbidResult();
//        }
//    }
//}

// --- Example Controller ---

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoriesController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditCategory(Guid id)
    {
        try
        {
            await _categoryService.EditCategoryAsync(id);
            return Ok(new { Message = "Category edited." });
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }
}

//// --- Program.cs minimal setup ---

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();

//builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<AbacAuthorizationService>();
//builder.Services.AddScoped<CategoryService>();

//builder.Services.AddDbContext<AbacDbContext>(opt => opt.UseInMemoryDatabase("AbacDb"));

//var app = builder.Build();

//await SeedData.InitializeAsync(app.Services);
//await ServiceActionRegistration.RegisterServiceActionsAsync(app.Services);

//app.MapControllers();

//app.Run();

