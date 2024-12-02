using ProjetV3R_Employe.Data.Models.ProjetV3R;

public class ProduitDto
{
    public ProduitDto()
    {
        Fournisseur = new Fournisseur();
        FournisseurNom = string.Empty;
        CommoditeTitreFr = string.Empty;
        ClasseTitreFr = string.Empty;
        FamilleTitreFr = string.Empty;
        SegmentTitreFr = string.Empty;
    }

    public int ProduitId { get; set; }
    public int FournisseurId { get; set; }
    public Fournisseur Fournisseur { get; set; }
    public string FournisseurNom { get; set; }
    public string CommoditeTitreFr { get; set; }
    public string ClasseTitreFr { get; set; }
    public string FamilleTitreFr { get; set; }
    public string SegmentTitreFr { get; set; }
    public DateTime Timestamps { get; set; }
}
