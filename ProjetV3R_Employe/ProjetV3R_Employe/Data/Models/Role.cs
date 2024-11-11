using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models;

public partial class Role
{
    public int IdRole { get; set; }

    public string NomRole { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
