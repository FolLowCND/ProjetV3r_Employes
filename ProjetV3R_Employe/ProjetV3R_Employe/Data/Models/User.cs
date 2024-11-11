using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public int Role { get; set; }

    public virtual Role RoleNavigation { get; set; } = null!;
}
