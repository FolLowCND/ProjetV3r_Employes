using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class Province
{
    public int ProvinceId { get; set; }

    public string CodeProvince { get; set; } = null!;

    public string NomProvince { get; set; } = null!;

    public virtual ICollection<Adress> Adresses { get; } = new List<Adress>();
}
