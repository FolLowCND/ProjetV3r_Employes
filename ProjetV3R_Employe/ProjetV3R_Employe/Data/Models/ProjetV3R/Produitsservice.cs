using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class Produitsservice
{
    public int ProduitId { get; set; }

    public int FournisseurId { get; set; }

    public int ComoditeId { get; set; }

    public DateTime Timestamps { get; set; }

    public virtual UnspscComodite Comodite { get; set; } = null!;

    public virtual Fournisseur Fournisseur { get; set; } = null!;
}
