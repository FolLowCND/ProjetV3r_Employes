using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetV3R_Employe.Data.Models;

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
        _dbContext.Users.Add(employe);
        await _dbContext.SaveChangesAsync();
    }

    public async Task ModifierEmployeAsync(User employe)
    {
        _dbContext.Users.Update(employe);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SupprimerEmployeAsync(int id)
    {
        var employe = await _dbContext.Users.FindAsync(id);
        if (employe != null)
        {
            _dbContext.Users.Remove(employe);
            await _dbContext.SaveChangesAsync();
        }
    }
}
