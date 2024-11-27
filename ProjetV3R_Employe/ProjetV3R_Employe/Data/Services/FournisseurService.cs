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

    public Action? OnFournisseursChanged { get; set; }

    // Get all Fournisseurs
    public async Task<List<Fournisseur>> ObtenirFournisseursAsync()
    {
        try
        {
            return await _dbContext.Fournisseurs.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de la récupération des fournisseurs : {ex.Message}");
        }
    }

    // Get a specific Fournisseur by ID
    public async Task<Fournisseur> ObtenirFournisseurParIdAsync(int id)
    {
        try
        {
            var fournisseur = await _dbContext.Fournisseurs.FirstOrDefaultAsync(f => f.FournisseurId == id);
            if (fournisseur == null)
            {
                throw new Exception("Le fournisseur demandé n'existe pas.");
            }

            return fournisseur;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de la récupération du fournisseur : {ex.Message}");
        }
    }

    // Add a new Fournisseur
    public async Task AjouterFournisseurAsync(Fournisseur fournisseur)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(fournisseur.NomEntreprise))
            {
                throw new ArgumentException("Le nom de l'entreprise est obligatoire.");
            }

            if (string.IsNullOrWhiteSpace(fournisseur.Neq))
            {
                throw new ArgumentException("Le NEQ est obligatoire.");
            }

            _dbContext.Fournisseurs.Add(fournisseur);
            await _dbContext.SaveChangesAsync();
            OnFournisseursChanged?.Invoke();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de l'ajout du fournisseur : {ex.Message}");
        }
    }

    // Update an existing Fournisseur
    public async Task ModifierFournisseurAsync(Fournisseur fournisseur)
    {
        try
        {
            var fournisseurExistant = await _dbContext.Fournisseurs.FirstOrDefaultAsync(f => f.FournisseurId == fournisseur.FournisseurId);
            if (fournisseurExistant == null)
            {
                throw new Exception("Le fournisseur à modifier n'existe pas.");
            }

            fournisseurExistant.NomEntreprise = fournisseur.NomEntreprise;
            fournisseurExistant.Neq = fournisseur.Neq;
            fournisseurExistant.EtatDemande = fournisseur.EtatDemande;
            fournisseurExistant.EtatCompte = fournisseur.EtatCompte;
            fournisseurExistant.CourrielEntreprise = fournisseur.CourrielEntreprise;
            fournisseurExistant.DetailsEntreprise = fournisseur.DetailsEntreprise;
            fournisseurExistant.SiteWeb = fournisseur.SiteWeb;

            await _dbContext.SaveChangesAsync();
            OnFournisseursChanged?.Invoke();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de la modification du fournisseur : {ex.Message}");
        }
    }

    // Delete a Fournisseur
    public async Task SupprimerFournisseurAsync(int fournisseurId)
    {
        try
        {
            var fournisseur = await _dbContext.Fournisseurs.FindAsync(fournisseurId);
            if (fournisseur == null)
            {
                throw new Exception("Le fournisseur n'existe pas.");
            }

            _dbContext.Fournisseurs.Remove(fournisseur);
            await _dbContext.SaveChangesAsync();
            OnFournisseursChanged?.Invoke();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de la suppression du fournisseur : {ex.Message}");
        }
    }
}
