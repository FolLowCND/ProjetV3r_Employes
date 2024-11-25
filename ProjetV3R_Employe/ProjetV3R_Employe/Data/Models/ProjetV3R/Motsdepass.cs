using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class Motsdepass
{
    public int MdpId { get; set; }

    public int UserId { get; set; }

    public string Mdp { get; set; } = null!;

    public string IpChangementMdp { get; set; } = null!;

    public DateTime Timestamps { get; set; }

    public virtual User User { get; set; } = null!;
}
