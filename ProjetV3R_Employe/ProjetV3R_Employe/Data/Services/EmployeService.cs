using ProjetV3R_Employe.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


public class EmployeService
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Action? OnEmployesChanged { get; set; }

    public async Task<List<User>> ObtenirEmployesAsync()
    {
        try
        {
            return await _dbContext.Users
                .Include(u => u.RoleNavigation)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de la récupération des employés : {ex.Message}");
        }
    }

    public async Task<List<Role>> ObtenirRolesAsync()
    {
        try
        {
            return await _dbContext.Roles.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de la récupération des rôles : {ex.Message}");
        }
    }

    public async Task AjouterEmployeAsync(User employe)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(employe.Email))
            {
                throw new ArgumentException("L'email est obligatoire.");
            }

            if (!new EmailAddressAttribute().IsValid(employe.Email))
            {
                throw new ArgumentException("L'email n'est pas valide.");
            }

            if (!await _dbContext.Roles.AnyAsync(r => r.IdRole == employe.Role))
            {
                throw new ArgumentException("Le rôle sélectionné est invalide.");
            }

            _dbContext.Users.Add(employe);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de l'ajout de l'employé : {ex.Message}");
        }
    }


    public async Task<User> ObtenirEmployeParIdAsync(int id)
    {
        try
        {
            var employe = await _dbContext.Users
                .Include(u => u.RoleNavigation)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (employe == null)
            {
                throw new Exception("L'employé demandé n'existe pas.");
            }

            return employe;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de la récupération de l'employé : {ex.Message}");
        }
    }

    public async Task ModifierEmployeAsync(User employe)
    {
        try
        {
            var employeExistant = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == employe.Id);

            if (employeExistant == null)
            {
                throw new Exception("L'employé à modifier n'existe pas.");
            }

            employeExistant.Role = employe.Role;
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de la modification de l'employé : {ex.Message}");
        }
    }

}
