using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProjetV3R_Employe.Data.Models;
using Microsoft.EntityFrameworkCore;
using ProjetV3R_Employe.Data;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using ProjetV3R_Employe.SignalR;

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
        private readonly AuthService _authService;
        private readonly IHubContext<ConnectionHub> _hubContext;

        public AuthController(ApplicationDbContext dbContext, AuthService authService, IHubContext<ConnectionHub> hubContext)
        {
            _dbContext = dbContext;
            _authService = authService;
            _hubContext = hubContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _dbContext.Users
                    .Include(u => u.RoleNavigation)
                    .FirstOrDefaultAsync(u => u.Email == request.Email);

                if (user == null || user.RoleNavigation == null)
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveLoginStatus", "Utilisateur ou rôle introuvable.");
                    return BadRequest(new { message = "Utilisateur ou rôle introuvable." });
                }

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                };

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.RoleNavigation.NomRole)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                // Envoyer un message au client connecté
                var connectionId = _hubContext.Clients.All; // Peut être remplacé par une logique spécifique
                await _hubContext.Clients.All.SendAsync("ReceiveLoginStatus", $"Connexion réussie pour {user.Email}");

                return Ok(new { role = user.RoleNavigation.NomRole });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AuthController.Login] Erreur : {ex.Message}");
                return StatusCode(500, new { message = "Erreur interne du serveur." });
            }
        }

    }
}
