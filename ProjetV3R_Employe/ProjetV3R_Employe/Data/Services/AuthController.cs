using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProjetV3R_Employe.Data.Models;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using System.ComponentModel.DataAnnotations; // Ajout pour validation des données

namespace ProjetV3R_Employe.Controllers
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string Role { get; set; } = string.Empty;
    }


    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                Console.WriteLine($"[AuthController.Login] Tentative de connexion pour l'email : {request.Email}");

                var user = await _dbContext.Users
                    .Include(u => u.RoleNavigation)
                    .FirstOrDefaultAsync(u => u.Email == request.Email);

                if (user == null)
                {
                    Console.WriteLine($"[AuthController.Login] Aucun utilisateur trouvé avec l'email : {request.Email}");
                    return BadRequest("Utilisateur introuvable.");
                }

                if (user.RoleNavigation == null)
                {
                    Console.WriteLine($"[AuthController.Login] Aucun rôle associé trouvé pour l'utilisateur.");
                    return BadRequest("Rôle introuvable.");
                }

                Console.WriteLine($"[AuthController.Login] Utilisateur trouvé : {user.Email}, Rôle : {user.RoleNavigation.NomRole}");

                // Créer un principal utilisateur pour le cookie
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

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return Ok(new { role = user.RoleNavigation.NomRole });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AuthController.Login] Erreur : {ex.Message}");
                return StatusCode(500, "Erreur interne du serveur.");
            }
        }
    }
}
