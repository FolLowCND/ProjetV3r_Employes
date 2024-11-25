using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly AuthService _authService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomAuthenticationStateProvider(AuthService authService, IHttpContextAccessor httpContextAccessor)
    {
        _authService = authService;
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext != null)
        {
            foreach (var cookie in httpContext.Request.Cookies)
            {
                Console.WriteLine($"[CustomAuthProider]Cookie reçu : {cookie.Key} = {cookie.Value}");
            }
        }

        if (httpContext?.User?.Identity?.IsAuthenticated == true)
        {
            Console.WriteLine("[CustomAuthProider]Utilisateur authentifié via SignalR avec cookies :");

                // Récupérer l'email de l'utilisateur connecté
                var email = httpContext.User.Identity.Name;

            if (!string.IsNullOrEmpty(email))
            {
                var user = await _authService.GetUserByEmailAsync(email);

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.Role, user.RoleNavigation.NomRole)
                    };

                    var identity = new ClaimsIdentity(claims, "CustomAuthentication");
                    var principal = new ClaimsPrincipal(identity);

                    return new AuthenticationState(principal);
                }

                Console.WriteLine("[CustomAuthProider]Utilisateur introuvable dans la base de données.");
            }
        }

        Console.WriteLine("[CustomAuthProider]Aucun utilisateur connecté via SignalR.");
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }


    public void MarkUserAsAuthenticated(string email, string role)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
        new Claim(ClaimTypes.Name, email),
        new Claim(ClaimTypes.Role, role)
    }, CookieAuthenticationDefaults.AuthenticationScheme));

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticatedUser)));
    }


    public void MarkUserAsLoggedOut()
    {
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
    }
}

