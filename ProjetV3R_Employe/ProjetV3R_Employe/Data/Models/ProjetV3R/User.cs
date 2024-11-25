using System;
using System.Collections.Generic;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class User
{
    public int UserId { get; set; }

    public int FournisseurId { get; set; }

    public string? Identifiant { get; set; }

    public DateTime DateCreation { get; set; }

    public virtual ICollection<Connexion> Connexions { get; } = new List<Connexion>();

    public virtual Fournisseur Fournisseur { get; set; } = null!;

    public virtual ICollection<Motsdepass> Motsdepasses { get; } = new List<Motsdepass>();
}
