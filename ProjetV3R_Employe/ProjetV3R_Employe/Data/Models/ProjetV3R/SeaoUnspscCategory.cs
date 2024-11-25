using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class SeaoUnspscCategory
{
    public int CategorieId { get; set; }

    public string CategorieCode { get; set; } = null!;

    public string? CategorieNom { get; set; }

    public string? NatureCode { get; set; }

    public virtual SeaoUnspscNature? NatureCodeNavigation { get; set; }

    public virtual ICollection<UnspscComodite> UnspscComodites { get; } = new List<UnspscComodite>();
}
