using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProjetV3R_Employe.Data.Models;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

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

        public AuthController(ApplicationDbContext dbContext, AuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
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

