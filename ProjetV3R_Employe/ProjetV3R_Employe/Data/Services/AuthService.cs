using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ProjetV3R_Employe.Data.Models;

namespace ProjetV3R_Employe.Data.Services
{
    public class AuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Méthode pour obtenir l'utilisateur actuellement connecté
        public async Task<User?> GetCurrentUserAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext?.User?.Identity?.IsAuthenticated == true)
            {
                var email = httpContext.User.Identity.Name;

                if (!string.IsNullOrEmpty(email))
                {
                    // Simule un utilisateur connecté. Remplace par une requête à la base de données si nécessaire.
                    var user = new User
                    {
                        Email = email,
                        Role = 1 // Exemple de rôle
                    };
                    return user;
                }
            }

            return null;
        }

        // Méthode pour déconnecter l'utilisateur
        public async Task SignOutAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                Console.WriteLine("[SignOutAsync] Utilisateur déconnecté.");
            }
        }
    }
}
