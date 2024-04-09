using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OAuthServer.Services;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;
using System.Collections.Immutable;
using System.Security.Claims;
using System.Web;
using Microsoft.Extensions.Primitives;

namespace OAuthServer.Controllers
{
    [ApiController]
    public class AuthorizationController : Controller
    {
        //private readonly IOpenIddictApplicationManager _applicationManager;
        //private readonly IOpenIddictScopeManager _scopeManager;
        //private readonly AuthorizationService _authService;
        //private readonly IOpenIddictAuthorizationManager _authorizationManager;

        //public AuthorizationController(
        //    IOpenIddictApplicationManager applicationManager,
        //    IOpenIddictScopeManager scopeManager,
        //    AuthorizationService authService,
        //    IOpenIddictAuthorizationManager authorizationManager)
        //{
        //    _applicationManager = applicationManager;
        //    _scopeManager = scopeManager;
        //    _authService = authService;
        //    _authorizationManager = authorizationManager;
        //}


        //    #region andreyka26's code
        //    [HttpGet("~/connect/authorize")]
        //    [HttpPost("~/connect/authorize")]
        //    public async Task<IActionResult> Authorize_Andrey()
        //    {
        //        var request = HttpContext.GetOpenIddictServerRequest() ??
        //                      throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        //        var application = await _applicationManager.FindByClientIdAsync(request.ClientId) ??
        //                          throw new InvalidOperationException("Details concerning the calling client application cannot be found.");

        //        if (await _applicationManager.GetConsentTypeAsync(application) != ConsentTypes.Explicit)
        //        {
        //            return Forbid(
        //                authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
        //                properties: new AuthenticationProperties(new Dictionary<string, string?>
        //                {
        //                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidClient,
        //                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
        //                        "Only clients with explicit consent type are allowed."
        //                }));
        //        }

        //        var parameters = _authService.ParseOAuthParameters(HttpContext, new List<string> { Parameters.Prompt });

        //        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //        if (!_authService.IsAuthenticated(result, request))
        //        {
        //            return Challenge(properties: new AuthenticationProperties
        //            {
        //                RedirectUri = _authService.BuildRedirectUrl(HttpContext.Request, parameters)
        //            }, new[] { CookieAuthenticationDefaults.AuthenticationScheme });
        //        }

        //        if (request.HasPrompt(Prompts.Login))
        //        {
        //            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //            return Challenge(properties: new AuthenticationProperties
        //            {
        //                RedirectUri = _authService.BuildRedirectUrl(HttpContext.Request, parameters)
        //            }, new[] { CookieAuthenticationDefaults.AuthenticationScheme });
        //        }

        //        //var consentClaim = result.Principal.GetClaim(Consts.ConsentNaming);

        //        //// it might be extended in a way that consent claim will contain list of allowed client ids.
        //        //if (consentClaim != Consts.GrantAccessValue || request.HasPrompt(Prompts.Consent))
        //        //{
        //        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //        //    var returnUrl = HttpUtility.UrlEncode(_authService.BuildRedirectUrl(HttpContext.Request, parameters));
        //        //    var consentRedirectUrl = $"/Consent?returnUrl={returnUrl}";

        //        //    return Redirect(consentRedirectUrl);
        //        //}


        //        var userId = result.Principal.FindFirst(ClaimTypes.Email)!.Value;

        //        var identity = new ClaimsIdentity(
        //            authenticationType: TokenValidationParameters.DefaultAuthenticationType,
        //            nameType: Claims.Name,
        //            roleType: Claims.Role);

        //        identity.SetClaim(Claims.Subject, userId)
        //            .SetClaim(Claims.Email, userId)
        //            .SetClaim(Claims.Name, userId)
        //            .SetClaims(Claims.Role, new List<string> { "user", "admin" }.ToImmutableArray());

        //        identity.SetScopes(request.GetScopes());
        //        identity.SetResources(await _scopeManager.ListResourcesAsync(identity.GetScopes()).ToListAsync());
        //        identity.SetDestinations(c => AuthorizationService.GetDestinations(identity, c));

        //        return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        //    }
        //    #endregion

        //    #region openiddict samples
        //    [HttpGet("~/connect/authorize")]
        //    [HttpPost("~/connect/authorize")]
        //    [IgnoreAntiforgeryToken]
        //    public async Task<IActionResult> Authorize()
        //    {
        //        var request = HttpContext.GetOpenIddictServerRequest() ??
        //            throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        //        // Try to retrieve the user principal stored in the authentication cookie and redirect
        //        // the user agent to the login page (or to an external provider) in the following cases:
        //        //
        //        //  - If the user principal can't be extracted or the cookie is too old.
        //        //  - If prompt=login was specified by the client application.
        //        //  - If a max_age parameter was provided and the authentication cookie is not considered "fresh" enough.
        //        //
        //        // For scenarios where the default authentication handler configured in the ASP.NET Core
        //        // authentication options shouldn't be used, a specific scheme can be specified here.
        //        var result = await HttpContext.AuthenticateAsync();
        //        if (result == null || !result.Succeeded || request.HasPrompt(Prompts.Login) ||
        //           (request.MaxAge != null && result.Properties?.IssuedUtc != null &&
        //            DateTimeOffset.UtcNow - result.Properties.IssuedUtc > TimeSpan.FromSeconds(request.MaxAge.Value)))
        //        {
        //            // If the client application requested promptless authentication,
        //            // return an error indicating that the user is not logged in.
        //            if (request.HasPrompt(Prompts.None))
        //            {
        //                return Forbid(
        //                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
        //                    properties: new AuthenticationProperties(new Dictionary<string, string>
        //                    {
        //                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.LoginRequired,
        //                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The user is not logged in."
        //                    }));
        //            }

        //            // To avoid endless login -> authorization redirects, the prompt=login flag
        //            // is removed from the authorization request payload before redirecting the user.
        //            var prompt = string.Join(" ", request.GetPrompts().Remove(Prompts.Login));

        //            var parameters = Request.HasFormContentType ?
        //                Request.Form.Where(parameter => parameter.Key != Parameters.Prompt).ToList() :
        //                Request.Query.Where(parameter => parameter.Key != Parameters.Prompt).ToList();

        //            parameters.Add(KeyValuePair.Create(Parameters.Prompt, new StringValues(prompt)));

        //            // For scenarios where the default challenge handler configured in the ASP.NET Core
        //            // authentication options shouldn't be used, a specific scheme can be specified here.
        //            return Challenge(new AuthenticationProperties
        //            {
        //                RedirectUri = Request.PathBase + Request.Path + QueryString.Create(parameters)
        //            });
        //        }

        //        // Retrieve the profile of the logged in user.
        //        var user = await _userManager.GetUserAsync(result.Principal) ??
        //            throw new InvalidOperationException("The user details cannot be retrieved.");

        //        // Retrieve the application details from the database.
        //        var application = await _applicationManager.FindByClientIdAsync(request.ClientId) ??
        //            throw new InvalidOperationException("Details concerning the calling client application cannot be found.");

        //        // Retrieve the permanent authorizations associated with the user and the calling client application.
        //        var authorizations = await _authorizationManager.FindAsync(
        //            subject: await _userManager.GetUserIdAsync(user),
        //            client: await _applicationManager.GetIdAsync(application),
        //            status: Statuses.Valid,
        //            type: AuthorizationTypes.Permanent,
        //            scopes: request.GetScopes()).ToListAsync();

        //        switch (await _applicationManager.GetConsentTypeAsync(application))
        //        {
        //            // If the consent is external (e.g when authorizations are granted by a sysadmin),
        //            // immediately return an error if no authorization can be found in the database.
        //            case ConsentTypes.External when authorizations.Count is 0:
        //                return Forbid(
        //                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
        //                    properties: new AuthenticationProperties(new Dictionary<string, string>
        //                    {
        //                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.ConsentRequired,
        //                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
        //                            "The logged in user is not allowed to access this client application."
        //                    }));

        //            // If the consent is implicit or if an authorization was found,
        //            // return an authorization response without displaying the consent form.
        //            case ConsentTypes.Implicit:
        //            case ConsentTypes.External when authorizations.Count is not 0:
        //            case ConsentTypes.Explicit when authorizations.Count is not 0 && !request.HasPrompt(Prompts.Consent):
        //                // Create the claims-based identity that will be used by OpenIddict to generate tokens.
        //                var identity = new ClaimsIdentity(
        //                    authenticationType: TokenValidationParameters.DefaultAuthenticationType,
        //                    nameType: Claims.Name,
        //                    roleType: Claims.Role);

        //                // Add the claims that will be persisted in the tokens.
        //                identity.SetClaim(Claims.Subject, await _userManager.GetUserIdAsync(user))
        //                        .SetClaim(Claims.Email, await _userManager.GetEmailAsync(user))
        //                        .SetClaim(Claims.Name, await _userManager.GetUserNameAsync(user))
        //                        .SetClaim(Claims.PreferredUsername, await _userManager.GetUserNameAsync(user))
        //                        .SetClaims(Claims.Role, (await _userManager.GetRolesAsync(user)).ToImmutableArray());

        //                // Note: in this sample, the granted scopes match the requested scope
        //                // but you may want to allow the user to uncheck specific scopes.
        //                // For that, simply restrict the list of scopes before calling SetScopes.
        //                identity.SetScopes(request.GetScopes());
        //                identity.SetResources(await _scopeManager.ListResourcesAsync(identity.GetScopes()).ToListAsync());

        //                // Automatically create a permanent authorization to avoid requiring explicit consent
        //                // for future authorization or token requests containing the same scopes.
        //                var authorization = authorizations.LastOrDefault();
        //                authorization ??= await _authorizationManager.CreateAsync(
        //                    identity: identity,
        //                    subject: await _userManager.GetUserIdAsync(user),
        //                    client: await _applicationManager.GetIdAsync(application),
        //                    type: AuthorizationTypes.Permanent,
        //                    scopes: identity.GetScopes());

        //                identity.SetAuthorizationId(await _authorizationManager.GetIdAsync(authorization));
        //                identity.SetDestinations(GetDestinations);

        //                return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        //            // At this point, no authorization was found in the database and an error must be returned
        //            // if the client application specified prompt=none in the authorization request.
        //            case ConsentTypes.Explicit when request.HasPrompt(Prompts.None):
        //            case ConsentTypes.Systematic when request.HasPrompt(Prompts.None):
        //                return Forbid(
        //                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
        //                    properties: new AuthenticationProperties(new Dictionary<string, string>
        //                    {
        //                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.ConsentRequired,
        //                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
        //                            "Interactive user consent is required."
        //                    }));

        //            // In every other case, render the consent form.
        //            default:
        //                return View(new AuthorizeViewModel
        //                {
        //                    ApplicationName = await _applicationManager.GetLocalizedDisplayNameAsync(application),
        //                    Scope = request.Scope
        //                });
        //        }
        //    }
        //}
        //    #endregion
    }
}
