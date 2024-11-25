using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class SeaoUnspscNature
{
    public int NatureId { get; set; }

    public string NatureCode { get; set; } = null!;

    public string? NatureNom { get; set; }

    public virtual ICollection<SeaoUnspscCategory> SeaoUnspscCategories { get; } = new List<SeaoUnspscCategory>();
}
