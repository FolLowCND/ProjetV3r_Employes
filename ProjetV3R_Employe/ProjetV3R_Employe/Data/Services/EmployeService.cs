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
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;


    public EmployeService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public EmployeService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<ProjetV3R_Employe.Data.Models.User>> ObtenirEmployesAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }


    public async Task AjouterEmployeAsync(ProjetV3R_Employe.Data.Models.User employe)
    {
        // Vérification de l'email
        if (!EstEmailValide(employe.Email))
        {
            throw new ArgumentException("L'email fourni n'est pas valide.");
        }

        _dbContext.Users.Add(employe);
        await _dbContext.SaveChangesAsync();
    }

    public async Task ModifierEmployeAsync(ProjetV3R_Employe.Data.Models.User employe)
    {
        _dbContext.Users.Update(employe);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SupprimerEmployeAsync(int id)
    {
        using var context = _dbContextFactory.CreateDbContext();
        var employe = await context.Users.FindAsync(id);
        if (employe != null)
        {
            context.Users.Remove(employe);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<Role>> ObtenirRolesAsync()
    {
        using var context = _dbContextFactory.CreateDbContext();
        return await context.Roles.ToListAsync();
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


    public async Task<User?> ObtenirEmployeParIdAsync(int id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

}
