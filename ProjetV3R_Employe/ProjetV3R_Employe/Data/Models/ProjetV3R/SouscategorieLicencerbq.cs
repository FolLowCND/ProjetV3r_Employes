using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class SouscategorieLicencerbq
{
    public int SousCategrorieRbqId { get; set; }

    public int IdLicence { get; set; }

    public int IdSousCategorie { get; set; }

    public virtual Licencesrbq IdLicenceNavigation { get; set; } = null!;

    public virtual Souscategoriesafter2008 IdSousCategorieNavigation { get; set; } = null!;
}
