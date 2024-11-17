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

        // M�thode pour obtenir l'utilisateur actuellement connect�
        public async Task<User?> GetCurrentUserAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext?.User?.Identity?.IsAuthenticated == true)
            {
                var email = httpContext.User.Identity.Name;

                if (!string.IsNullOrEmpty(email))
                {
                    // Simule un utilisateur connect�. Remplace par une requ�te � la base de donn�es si n�cessaire.
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

        // M�thode pour d�connecter l'utilisateur
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
}
