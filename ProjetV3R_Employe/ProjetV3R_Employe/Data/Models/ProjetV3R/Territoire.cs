using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class Territoire
{
    public int TerritoireId { get; set; }

    public string CodeRegionAdministrative { get; set; } = null!;

    public string CodeTerritoire { get; set; } = null!;

    public string NomTerritoire { get; set; } = null!;

    public virtual Regionadministrative CodeRegionAdministrativeNavigation { get; set; } = null!;

    public virtual ICollection<Municipalite> Municipalites { get; } = new List<Municipalite>();
}
