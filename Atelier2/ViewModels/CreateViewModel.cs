using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Atelier2.Models;

namespace Atelier2.ViewModels
{
    public class CreateViewModel
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Nom du produit :")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Prix en dinar :")]
        public float Price { get; set; }

        [Required]
        [Display(Name = "Quantité en unité :")]
        public int QteStock { get; set; }

        [Required(ErrorMessage = "Veuillez sélectionner une catégorie.")]
        [Display(Name = "Catégorie :")]
        public int CategoryId { get; set; }

       
        public Category? Category { get; set; }

        [Display(Name = "Image :")]
        public IFormFile? ImagePath { get; set; } // champ d’upload
    }
}
