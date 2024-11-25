using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class Contact
{
    public int ContactId { get; set; }

    public int FournisseurId { get; set; }

    public string NomContact { get; set; } = null!;

    public string PrenomContact { get; set; } = null!;

    public string FonctionContact { get; set; } = null!;

    public string CourrielContact { get; set; } = null!;

    public string? TypeTel { get; set; }

    public string NumTelContact { get; set; } = null!;

    public string? PosteTelContact { get; set; }

    public DateTime Timestamps { get; set; }

    public virtual Fournisseur Fournisseur { get; set; } = null!;
}
