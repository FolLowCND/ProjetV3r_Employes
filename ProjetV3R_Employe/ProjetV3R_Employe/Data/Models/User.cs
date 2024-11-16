using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models;

public partial class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public int Role { get; set; }
    public Role RoleNavigation { get; set; } = null!;
}
