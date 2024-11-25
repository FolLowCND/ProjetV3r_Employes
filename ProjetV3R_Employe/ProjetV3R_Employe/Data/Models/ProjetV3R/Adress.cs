using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class Adress
{
    public int AdresseId { get; set; }

    public int FournisseurId { get; set; }

    public string NumeroCivique { get; set; } = null!;

    public string Rue { get; set; } = null!;

    public string? Bureau { get; set; }

    public string? CodeMunicipalite { get; set; }

    public string CodeProvince { get; set; } = null!;

    public string CodePostal { get; set; } = null!;

    public string NumTel { get; set; } = null!;

    public string? Poste { get; set; }

    public DateTime Timestamps { get; set; }

    public string? NomMunicipalite { get; set; }

    public virtual Municipalite? CodeMunicipaliteNavigation { get; set; }

    public virtual Province CodeProvinceNavigation { get; set; } = null!;

    public virtual Fournisseur Fournisseur { get; set; } = null!;
}
