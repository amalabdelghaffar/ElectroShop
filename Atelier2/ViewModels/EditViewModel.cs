using Atelier2.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Atelier2.ViewModel
{
    public class EditViewModel : CreateViewModel
    {
        public int ProductId { get; set; }
        public string? ExistingImagePath { get; set; }
       
        public IFormFile? Image { get; set; } 
    }
}

