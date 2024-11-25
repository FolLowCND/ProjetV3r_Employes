using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class UnspscSegment
{
    public int SegmentId { get; set; }

    public string SegmentNombre { get; set; } = null!;

    public string SegmentTitreEn { get; set; } = null!;

    public string SegmentTitreFr { get; set; } = null!;

    public virtual ICollection<UnspscFamille> UnspscFamilles { get; } = new List<UnspscFamille>();
}
