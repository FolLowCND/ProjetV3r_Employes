using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ProjetV3R_Employe.Data.Models;
using System.Security.Claims;

public class AuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task SignInAsync(string email, string role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Role, role)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
        };

        Console.WriteLine($"Expiration du cookie : {authProperties.ExpiresUtc}");

        await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }

    public async Task<User?> GetCurrentUserAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var email = httpContext.User.Identity.Name;

            if (!string.IsNullOrEmpty(email))
            {
                // Simule un utilisateur connect�
                var user = new User
                {
                    Email = email,
                    Role = 1 // Exemple de r�le
                };
                return user;
            }
        }

        return null;
    }

    public async Task SignOutAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext != null)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Console.WriteLine("[SignOutAsync] Utilisateur d�connect�.");
        }
    }
}
