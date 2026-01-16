using System.ComponentModel.DataAnnotations;

namespace Atelier2.Models
{
    public class ContactViewModel
    {
        [Required, StringLength(80)]
        [Display(Name = "Nom complet")]
        public string? Name { get; set; }

        [Required, EmailAddress]
        [Display(Name = "Adresse e-mail")]
        public string? Email { get; set; }

        [StringLength(100)]
        [Display(Name = "Sujet")]
        public string? Subject { get; set; }

        [Required, StringLength(1000)]
        [Display(Name = "Message")]
        public string? Message { get; set; }
    }
}
