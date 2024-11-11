using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models;

public partial class Email
{
    public int IdEmail { get; set; }

    public string TitreEmail { get; set; } = null!;

    public string ObjetEmail { get; set; } = null!;

    public string FromEmail { get; set; } = null!;

    public string CcEmail { get; set; } = null!;

    public string BodyEmail { get; set; } = null!;
}
