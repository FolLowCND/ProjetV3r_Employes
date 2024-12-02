using ProjetV3R_Employe.Data.Models.ProjetV3R;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


public class ProduitsServicesService
{
    private readonly ApplicationDbContext2 _dbContext;

    public ProduitsServicesService(ApplicationDbContext2 dbContext)
    {
        _dbContext = dbContext;
    }

    //public async Task<List<ProduitDto>> GetProduitsAsync()
    //{
    //    return await _dbContext.Produitsservices
    //        .Include(ps => ps.Fournisseur)
    //        .Include(ps => ps.Comodite)
    //            .ThenInclude(c => c.ClasseNombreNavigation)
    //            .ThenInclude(cls => cls.FamilleNombreNavigation)
    //            .ThenInclude(fam => fam.SegmentNombreNavigation)
    //        .Select(ps => new ProduitDto
    //        {
    //            ProduitId = ps.ProduitId,
    //            FournisseurNom = ps.Fournisseur.NomEntreprise,
    //            CommoditeTitreFr = ps.Comodite.ComoditeTitreFr,
    //            ClasseTitreFr = ps.Comodite.ClasseNombreNavigation.ClasseTitreFr,
    //            FamilleTitreFr = ps.Comodite.ClasseNombreNavigation.FamilleNombreNavigation.FamilleTitreFr,
    //            SegmentTitreFr = ps.Comodite.ClasseNombreNavigation.FamilleNombreNavigation.SegmentNombreNavigation.SegmentTitreFr,
    //            Timestamps = ps.Timestamps
    //        })
    //        .ToListAsync();
    //}

    public async Task<List<ProduitDto>> GetProduitsAsync()
    {
        return await _dbContext.Produitsservices
            .Include(ps => ps.Fournisseur)
            .Include(ps => ps.Comodite)
                .ThenInclude(c => c.ClasseNombreNavigation)
                .ThenInclude(cls => cls.FamilleNombreNavigation)
                .ThenInclude(fam => fam.SegmentNombreNavigation)
            .Select(ps => new ProduitDto
            {
                ProduitId = ps.ProduitId,
                Fournisseur = ps.Fournisseur, // Ajout de l'objet Fournisseur complet
                FournisseurNom = ps.Fournisseur.NomEntreprise,
                CommoditeTitreFr = ps.Comodite.ComoditeTitreFr,
                ClasseTitreFr = ps.Comodite.ClasseNombreNavigation.ClasseTitreFr,
                FamilleTitreFr = ps.Comodite.ClasseNombreNavigation.FamilleNombreNavigation.FamilleTitreFr,
                SegmentTitreFr = ps.Comodite.ClasseNombreNavigation.FamilleNombreNavigation.SegmentNombreNavigation.SegmentTitreFr,
                Timestamps = ps.Timestamps
            })
            .ToListAsync();
    }



}