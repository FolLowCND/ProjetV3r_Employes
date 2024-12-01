public class ProduitDto
{
    public ProduitDto()
    {
        FournisseurNom = string.Empty;
        CommoditeTitreFr = string.Empty;
        ClasseTitreFr = string.Empty;
        FamilleTitreFr = string.Empty;
        SegmentTitreFr = string.Empty;
    }

    public int ProduitId { get; set; }
    public string FournisseurNom { get; set; }
    public string CommoditeTitreFr { get; set; }
    public string ClasseTitreFr { get; set; }
    public string FamilleTitreFr { get; set; }
    public string SegmentTitreFr { get; set; }
    public DateTime Timestamps { get; set; }
}
