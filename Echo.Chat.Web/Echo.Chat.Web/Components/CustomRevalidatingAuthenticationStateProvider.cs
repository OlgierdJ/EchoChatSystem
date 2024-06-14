using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Security.Claims;

namespace EchoWebapp.Components;

public class CustomRevalidatingAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly PersistentComponentState _state;
    private readonly JwtBearerOptions _options;

    private readonly PersistingComponentStateSubscription _subscription;

    private Task<AuthenticationState>? _authenticationStateTask;

    public CustomRevalidatingAuthenticationStateProvider(
        ILoggerFactory loggerFactory,
        IServiceScopeFactory scopeFactory,
        PersistentComponentState state,
        IOptions<JwtBearerOptions> options)
        : base(loggerFactory)
    {
        _scopeFactory = scopeFactory;
        _state = state;
        _options = options.Value;

        AuthenticationStateChanged += OnAuthenticationStateChanged;
        _subscription = state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);
    }

    protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

    protected override async Task<bool> ValidateAuthenticationStateAsync(
        AuthenticationState authenticationState, CancellationToken cancellationToken)
    {
        // Get the user manager from a new scope to ensure it fetches fresh data
        await using var scope = _scopeFactory.CreateAsyncScope();
        return ValidateSecurityStampAsync(authenticationState.User);
    }

    private bool ValidateSecurityStampAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity?.IsAuthenticated is false)
        {
            return false;
        }

        return true;
    }

    private void OnAuthenticationStateChanged(Task<AuthenticationState> authenticationStateTask)
    {
        _authenticationStateTask = authenticationStateTask;
    }

    private async Task OnPersistingAsync()
    {
        if (_authenticationStateTask is null)
        {
            throw new UnreachableException($"Authentication state not set in {nameof(RevalidatingServerAuthenticationStateProvider)}.{nameof(OnPersistingAsync)}().");
        }

        var authenticationState = await _authenticationStateTask;
        var principal = authenticationState.User;

        if (principal.Identity?.IsAuthenticated == true)
        {
            var userId = principal.FindFirst("sub")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                _state.PersistAsJson(nameof(UserInfo), new UserInfo
                {
                    UserId = userId,
                });
            }
        }
    }

    protected override void Dispose(bool disposing)
    {
        _subscription.Dispose();
        AuthenticationStateChanged -= OnAuthenticationStateChanged;
        base.Dispose(disposing);
    }


    #region old code
    //public TestUserType UserType { get; private set; } = TestUserType.None;

    //private ClaimsPrincipal Admin
    //{
    //    get
    //    {
    //        var identity = new ClaimsIdentity(new[]
    //        {
    //            new Claim(ClaimTypes.Sid, "985fdabb-5e4e-4637-b53a-d331a3158680"),
    //            new Claim(ClaimTypes.Name, "Administrator"),
    //            new Claim(ClaimTypes.Role, "Admin")
    //        }, "Test authentication type");
    //        return new ClaimsPrincipal(identity);
    //    }
    //}

    //private ClaimsPrincipal User
    //{
    //    get
    //    {
    //        var identity = new ClaimsIdentity(new[]
    //        {
    //            new Claim(ClaimTypes.Sid, "024672e0-250a-46fc-bd35-1902974cf9e1"),
    //            new Claim(ClaimTypes.Name, "Normal User"),
    //            new Claim(ClaimTypes.Role, "User")
    //        }, "Test authentication type");
    //        return new ClaimsPrincipal(identity);
    //    }
    //}

    //private ClaimsPrincipal Visitor
    //{
    //    get
    //    {
    //        var identity = new ClaimsIdentity(new[]
    //        {
    //            new Claim(ClaimTypes.Sid, "3ef75379-69d6-4f8b-ab5f-857c32775571"),
    //            new Claim(ClaimTypes.Name, "Visitor"),
    //            new Claim(ClaimTypes.Role, "Visitor")
    //        }, "Test authentication type");
    //        return new ClaimsPrincipal(identity);
    //    }
    //}

    //private ClaimsPrincipal Anonymous
    //{
    //    get
    //    {
    //        var identity = new ClaimsIdentity(new[]
    //        {
    //            new Claim(ClaimTypes.Sid, "0ade1e94-b50e-46cc-b5f1-319a96a6d92f"),
    //            new Claim(ClaimTypes.Name, "Anonymous"),
    //            new Claim(ClaimTypes.Role, "Anonymous")
    //        }, null);
    //        return new ClaimsPrincipal(identity);
    //    }
    //}

    //public override Task<AuthenticationState> GetAuthenticationStateAsync()
    //{
    //    var task = this.UserType switch
    //    {
    //        TestUserType.Admin => Task.FromResult(new AuthenticationState(this.Admin)),
    //        TestUserType.User => Task.FromResult(new AuthenticationState(this.User)),
    //        TestUserType.None => Task.FromResult(new AuthenticationState(this.Anonymous)),
    //        _ => Task.FromResult(new AuthenticationState(this.Visitor))
    //    };
    //    return task;
    //}

    //public Task<AuthenticationState> ChangeUser(TestUserType userType)
    //{
    //    this.UserType = userType;
    //    var task = this.GetAuthenticationStateAsync();
    //    this.NotifyAuthenticationStateChanged(task);
    //    return task;
    //}
    #endregion
}

public class UserInfo
{
    public string UserId { get; set; }
}

public enum TestUserType
{
    None,
    Visitor,
    User,
    Admin
}
