//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Mvc.Routing;
//using System.Reflection;

//namespace Echo.Chat.API;

//public class gptTest
//{
//}

//// ABAC Admin Panel Seed Project - ASP.NET Core + EF Core

//// --- Models ---

//public class Policy
//{
//    public Guid Id { get; set; }
//    public string Name { get; set; }
//    public string ResourceType { get; set; }
//    public bool RequireAllConditions { get; set; } = true;
//    public List<PolicyAction> Actions { get; set; } = new();
//    public List<PolicyCondition> Conditions { get; set; } = new();
//}

//public class PolicyAction
//{
//    public Guid Id { get; set; }
//    public Guid PolicyId { get; set; }
//    public string ActionName { get; set; }
//}

//public class PolicyCondition
//{
//    public Guid Id { get; set; }
//    public Guid PolicyId { get; set; }
//    public string? SubjectAttribute { get; set; }
//    public string? ResourceAttribute { get; set; }
//    public string Operator { get; set; }
//    public string? Value { get; set; }
//}

//public class RegisteredAction
//{
//    public Guid Id { get; set; }
//    public string Route { get; set; } = default!;
//    public string HttpMethod { get; set; } = default!;
//    public string? ResourceType { get; set; }
//    public List<string> Tags { get; set; } = new();
//}

//// --- DbContext ---

//public class AbacDbContext : DbContext
//{
//    public DbSet<Policy> Policies { get; set; }
//    public DbSet<PolicyAction> PolicyActions { get; set; }
//    public DbSet<PolicyCondition> PolicyConditions { get; set; }
//    public DbSet<RegisteredAction> RegisteredActions { get; set; }

//    public AbacDbContext(DbContextOptions<AbacDbContext> options) : base(options) { }
//}

//// --- Custom Attribute ---

//[AttributeUsage(AttributeTargets.Method)]
//public class AuthorizeActionAttribute : Attribute, IAsyncAuthorizationFilter
//{
//    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
//    {
//        var path = context.HttpContext.Request.Path.ToString();
//        var method = context.HttpContext.Request.Method;

//        var db = context.HttpContext.RequestServices.GetRequiredService<AbacDbContext>();
//        var registered = await db.RegisteredActions.FirstOrDefaultAsync(a => a.Route == path && a.HttpMethod == method);

//        if (registered is null || !registered.Tags.Any())
//        {
//            context.Result = new ForbidResult();
//            return;
//        }

//        // Simulate subject/resource lookup (normally from context/db)
//        var subject = new Dictionary<string, object> { ["Role"] = "HR", ["IsAdmin"] = "true" };
//        var resource = new Dictionary<string, object> { ["Type"] = registered.ResourceType, ["CategoryLevel"] = "top" };

//        var policies = await db.Policies
//            .Include(p => p.Actions)
//            .Include(p => p.Conditions)
//            .Where(p => p.ResourceType == registered.ResourceType && p.Actions.Any(a => registered.Tags.Contains(a.ActionName)))
//            .ToListAsync();

//        var allowed = policies.Any(p =>
//        {
//            var matches = p.Conditions.Select(cond =>
//            {
//                string? left = cond.SubjectAttribute != null ? subject.GetValueOrDefault(cond.SubjectAttribute)?.ToString()
//                            : cond.ResourceAttribute != null ? resource.GetValueOrDefault(cond.ResourceAttribute)?.ToString()
//                            : null;
//                return cond.Operator switch
//                {
//                    "Equals" => left == cond.Value,
//                    _ => false
//                };
//            });
//            return p.RequireAllConditions ? matches.All(x => x) : matches.Any(x => x);
//        });

//        if (!allowed)
//        {
//            context.Result = new ForbidResult();
//        }
//    }
//}

//// --- Startup Reflection Scanner ---

//public static class ActionRegistrationScanner
//{
//    public static async Task RegisterActionsAsync(WebApplication app)
//    {
//        using var scope = app.Services.CreateScope();
//        var db = scope.ServiceProvider.GetRequiredService<AbacDbContext>();

//        var controllers = Assembly.GetExecutingAssembly()
//            .GetTypes()
//            .Where(t => typeof(ControllerBase).IsAssignableFrom(t));

//        foreach (var controller in controllers)
//        {
//            var controllerRoute = controller.GetCustomAttribute<RouteAttribute>()?.Template ?? "api/[controller]";
//            controllerRoute = controllerRoute.Replace("[controller]", controller.Name.Replace("Controller", "").ToLower());

//            var methods = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public)
//                .Where(m => m.GetCustomAttributes().Any(a => a is HttpMethodAttribute));

//            foreach (var method in methods)
//            {
//                var httpAttr = method.GetCustomAttributes<HttpMethodAttribute>().FirstOrDefault();
//                var routeAttr = method.GetCustomAttribute<RouteAttribute>();
//                var fullRoute = controllerRoute;
//                if (routeAttr != null) fullRoute += "/" + routeAttr.Template;
//                fullRoute = fullRoute.ToLower();

//                if (!db.RegisteredActions.Any(a => a.Route == fullRoute && a.HttpMethod == httpAttr!.HttpMethods.First()))
//                {
//                    db.RegisteredActions.Add(new RegisteredAction
//                    {
//                        Id = Guid.NewGuid(),
//                        Route = fullRoute,
//                        HttpMethod = httpAttr!.HttpMethods.First(),
//                        ResourceType = InferResourceType(fullRoute),
//                        Tags = new List<string>() // Tags must be assigned by admin later
//                    });
//                }
//            }
//        }

//        await db.SaveChangesAsync();
//    }

//    private static string? InferResourceType(string route)
//    {
//        if (route.Contains("categories")) return "category";
//        if (route.Contains("users")) return "user";
//        return null;
//    }
//}

//// --- Controller Example ---

//[ApiController]
//[Route("api/policies")]
//public class PoliciesController : ControllerBase
//{
//    private readonly AbacDbContext _db;
//    public PoliciesController(AbacDbContext db) => _db = db;

//    [HttpGet]
//    public async Task<IActionResult> Get() => Ok(await _db.Policies.Include(p => p.Actions).Include(p => p.Conditions).ToListAsync());

//    [HttpPost]
//    public async Task<IActionResult> Create(Policy policy)
//    {
//        policy.Id = Guid.NewGuid();
//        foreach (var action in policy.Actions) action.Id = Guid.NewGuid();
//        foreach (var condition in policy.Conditions) condition.Id = Guid.NewGuid();
//        _db.Policies.Add(policy);
//        await _db.SaveChangesAsync();
//        return Ok(policy);
//    }
//}

//[ApiController]
//[Route("api/categories")]
//public class CategoriesController : ControllerBase
//{
//    [HttpPut("{id}")]
//    [AuthorizeAction]
//    public IActionResult EditCategory(Guid id)
//    {
//        // Dummy success response
//        return Ok(new { Message = "Category edited." });
//    }
//}
