using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetV3R_Employe.Data.Models;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Internal;

public class EmployeService
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> ObtenirEmployesAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task AjouterEmployeAsync(User employe)
    {
        try
        {
            if (!EstEmailValide(employe.Email))
            {
                throw new ArgumentException("L'email fourni n'est pas valide.");
            }

            _dbContext.Users.Add(employe);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Une erreur est survenue lors de l'ajout de l'employé : " + ex.Message);
        }
    }

    public async Task ModifierEmployeAsync(User employe)
    {
        try
        {
            _dbContext.Users.Update(employe);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Une erreur est survenue lors de la modification de l'employé : " + ex.Message);
        }
    }

    public async Task SupprimerEmployeAsync(int id)
    {
        try
        {
            var employe = await _dbContext.Users.FindAsync(id);
            if (employe != null)
            {
                _dbContext.Users.Remove(employe);
                await _dbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Une erreur est survenue lors de la suppression de l'employé : " + ex.Message);
        }
    }

    public async Task<List<Role>> ObtenirRolesAsync()
    {
        return await _dbContext.Roles.ToListAsync();
    }

    public async Task<User?> ObtenirEmployeParIdAsync(int id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    private bool EstEmailValide(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    public async Task<bool> ExisteEmailAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> RoleValideAsync(string role)
    {
        return await _dbContext.Roles.AnyAsync(r => r.NomRole == role);
    }
}
