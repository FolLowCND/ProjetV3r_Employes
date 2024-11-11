using System.ComponentModel.DataAnnotations;

namespace ProjetV3R_Employe.Data.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        private class UserMetadata
        {
            [Required(ErrorMessage = "L'email est obligatoire.")]
            [EmailAddress(ErrorMessage = "Veuillez entrer une adresse email valide.")]
            [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Le format de l'email est invalide.")]
            public string? Email { get; set; }
        }
    }
}
