using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class Municipalite
{
    public int MunicipaliteId { get; set; }

    public string CodeTerritoire { get; set; } = null!;

    public string CodeMunicipalite { get; set; } = null!;

    public string NomMunicipalite { get; set; } = null!;

    public virtual ICollection<Adress> Adresses { get; } = new List<Adress>();

    public virtual Territoire CodeTerritoireNavigation { get; set; } = null!;
}
