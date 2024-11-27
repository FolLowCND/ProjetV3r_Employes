using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetV3R_Employe.Data.Models.ProjetV3R;

public class FournisseurService
{
    private readonly ApplicationDbContext2 _dbContext;

    public FournisseurService(ApplicationDbContext2 dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Fournisseur>> ObtenirFournisseursAsync()
    {
        try
        {
            return await _dbContext.Fournisseurs.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la récupération des fournisseurs : {ex.Message}");
            return new List<Fournisseur>();
        }
    }

    public async Task<Fournisseur?> ObtenirFournisseurParIdAsync(int id)
    {
        try
        {
            return await _dbContext.Fournisseurs.FirstOrDefaultAsync(f => f.FournisseurId == id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la récupération du fournisseur : {ex.Message}");
            return null;
        }
    }

}