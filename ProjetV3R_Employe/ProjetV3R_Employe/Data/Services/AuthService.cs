using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjetV3R_Employe.Data.Models;
using System.Security.Claims;
using System.Threading.Tasks;


public class AuthService
{
    private readonly ApplicationDbContext _dbContext;
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
            .Include(u => u.RoleNavigation)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user != null)
        {
            // Marquer l'utilisateur comme authentifié dans CustomAuthenticationStateProvider
            var customAuthStateProvider = (CustomAuthenticationStateProvider)_httpContextAccessor.HttpContext
                .RequestServices.GetService<AuthenticationStateProvider>();

            customAuthStateProvider?.MarkUserAsAuthenticated(user.Email);

            // Créer un principal utilisateur pour le cookie
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.RoleNavigation.NomRole)
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Maintenir la session après fermeture du navigateur
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1) // Expiration après 1 heure
            };

            // Configurer les cookies
            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return true;
        }

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
                return await _dbContext.Users
                    .Include(u => u.RoleNavigation)
                    .FirstOrDefaultAsync(u => u.Email == email);
            }
        }

        Console.WriteLine("[GetCurrentUserAsync] Aucun utilisateur connecté ou email vide.");
        return null;
    }



}
