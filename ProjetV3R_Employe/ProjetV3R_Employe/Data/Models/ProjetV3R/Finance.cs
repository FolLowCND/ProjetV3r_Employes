using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class Finance
{
    public int FinanceId { get; set; }

    public int FournisseurId { get; set; }

    public string Tvq { get; set; } = null!;

    public string Tps { get; set; } = null!;

    public string CodeConditionPaiement { get; set; } = null!;

    public string Devise { get; set; } = null!;

    public string ModeCom { get; set; } = null!;

    public DateTime Timestamps { get; set; }

    public virtual Conditionspaiement CodeConditionPaiementNavigation { get; set; } = null!;

    public virtual Fournisseur Fournisseur { get; set; } = null!;
}
