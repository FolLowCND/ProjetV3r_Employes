using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjetV3R_Employe.Data.Models;
using System.Threading.Tasks;
private readonly ApplicationDbContext _dbContext;

public class AuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public async Task SignOutAsync()
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var customAuthStateProvider = (CustomAuthenticationStateProvider)_httpContextAccessor.HttpContext
                .RequestServices.GetService<AuthenticationStateProvider>();

            customAuthStateProvider?.MarkUserAsLoggedOut();
        }
    }


    public async Task<bool> LoginUserAsync(string email, string password)
    {
        var user = await _dbContext.Users
            .Include(u => u.RoleNavigation) // Charger le rôle
            .FirstOrDefaultAsync(u => u.Email == email && u.Password == password); // Vérifie l'email et le mot de passe

        if (user != null)
        {
            // Authentification réussie, marquer l'utilisateur comme authentifié
            var customAuthStateProvider = (CustomAuthenticationStateProvider)_httpContextAccessor.HttpContext
                .RequestServices.GetService<AuthenticationStateProvider>();

            customAuthStateProvider?.MarkUserAsAuthenticated(user.Email);

            // Rediriger ou continuer les opérations de connexion
            return true;
        }

        // Authentification échouée
        return false;
    }


    public async Task<User?> GetCurrentUserAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var email = httpContext.User.Identity.Name;

            if (!string.IsNullOrEmpty(email))
            {
                var dbContext = httpContext.RequestServices.GetService<ApplicationDbContext>();
                if (dbContext == null)
                {
                    Console.WriteLine("[GetCurrentUserAsync] ApplicationDbContext est null !");
                    return null;
                }

                // Charger l'utilisateur avec le rôle associé
                return await dbContext.Users
                    .Include(u => u.RoleNavigation)
                    .FirstOrDefaultAsync(u => u.Email == email);
            }
        }

        Console.WriteLine("[GetCurrentUserAsync] Aucun utilisateur connecté ou email vide.");
        return null;
    }



}
