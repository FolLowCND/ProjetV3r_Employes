using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class Brochure
{
    public int BrochureId { get; set; }

    public int FournisseurId { get; set; }

    public string? NomFichier { get; set; }

    public string TypeFichier { get; set; } = null!;

    public string NoFichier { get; set; } = null!;

    public int? Taille { get; set; }

    public string LienDocument { get; set; } = null!;

    public DateTime DateCreation { get; set; }

    public virtual Fournisseur Fournisseur { get; set; } = null!;
}
