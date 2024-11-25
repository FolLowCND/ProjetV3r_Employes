using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class Souscategoriesafter2008
{
    public int SousCategorieAfter2008Id { get; set; }

    public string NumeroSousCategorieAfter2008 { get; set; } = null!;

    public string NomSousCategorieAfter2008 { get; set; } = null!;

    public virtual ICollection<SouscategorieLicencerbq> SouscategorieLicencerbqs { get; } = new List<SouscategorieLicencerbq>();
}
