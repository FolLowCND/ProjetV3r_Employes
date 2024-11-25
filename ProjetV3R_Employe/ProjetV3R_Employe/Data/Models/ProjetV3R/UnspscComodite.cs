using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class UnspscComodite
{
    public int ComoditeId { get; set; }

    public string? ClasseNombre { get; set; }

    public string ComoditeNombre { get; set; } = null!;

    public string? ComoditeTitreEn { get; set; }

    public string ComoditeTitreFr { get; set; } = null!;

    public string? CategorieSeao { get; set; }

    public virtual SeaoUnspscCategory? CategorieSeaoNavigation { get; set; }

    public virtual UnspscClass? ClasseNombreNavigation { get; set; }

    public virtual ICollection<Produitsservice> Produitsservices { get; } = new List<Produitsservice>();
}
