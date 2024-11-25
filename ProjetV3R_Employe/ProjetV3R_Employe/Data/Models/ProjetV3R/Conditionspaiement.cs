using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class Conditionspaiement
{
    public int ConditionsPaiementsId { get; set; }

    public string CodeConditionsPaiements { get; set; } = null!;

    public string NomConditionsPaiements { get; set; } = null!;

    public virtual ICollection<Finance> Finances { get; } = new List<Finance>();
}
