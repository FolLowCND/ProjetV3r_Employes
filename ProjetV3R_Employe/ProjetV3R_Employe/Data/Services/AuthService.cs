using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using ProjetV3R_Employe.Data.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


public class AuthService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public async Task<(bool success, string? role, string? error)> LoginUserAsync(string email)
    {
        try
        {
            Console.WriteLine($"[LoginUserAsync] Tentative de connexion pour l'email : {email}");

            var user = await _dbContext.Users
                .Include(u => u.RoleNavigation)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                Console.WriteLine($"[LoginUserAsync] Aucun utilisateur trouv� avec l'email : {email}");
                return (false, null, "Utilisateur introuvable");
            }

            if (user.RoleNavigation == null)
            {
                Console.WriteLine($"[LoginUserAsync] Aucun r�le associ� trouv� pour l'utilisateur : {email}");
                return (false, null, "R�le introuvable");
            }

            Console.WriteLine($"[LoginUserAsync] Utilisateur trouv� : {user.Email}, R�le : {user.RoleNavigation.NomRole}");

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext.Response.HasStarted)
            {
                Console.WriteLine("[LoginUserAsync] La r�ponse HTTP a d�j� commenc�. Impossible de configurer les cookies.");
                return (false, null, "Impossible de configurer les cookies.");
            }

            // Cr�er un principal utilisateur pour le cookie
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.RoleNavigation.NomRole)
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
            };

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return (true, user.RoleNavigation.NomRole, null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[LoginUserAsync] Erreur lors de la connexion : {ex.Message}");
            return (false, null, "Erreur lors de la connexion.");
        }
    }





    public async Task SignOutAsync()
    {
        try
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                var customAuthStateProvider = (CustomAuthenticationStateProvider)_httpContextAccessor.HttpContext
                    .RequestServices.GetService<AuthenticationStateProvider>();

                customAuthStateProvider?.MarkUserAsLoggedOut();
                Console.WriteLine("[SignOutAsync] Utilisateur d�connect�.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SignOutAsync] Erreur lors de la d�connexion : {ex.Message}");
        }
    }

    public async Task<User?> GetCurrentUserAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var email = httpContext.User.Identity.Name;

            if (!string.IsNullOrEmpty(email))
            {
                return await _dbContext.Users
                    .Include(u => u.RoleNavigation)
                    .FirstOrDefaultAsync(u => u.Email == email);
            }
        }

        return null;
    }
}
