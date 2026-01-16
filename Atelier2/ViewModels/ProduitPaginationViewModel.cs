using Atelier2.Models;

namespace Atelier2.ViewModels
{
    public class ProduitPaginationViewModel
    {
        public List<Product> Products { get; set; }
        public int PageActuelle { get; set; }
        public int TotalPages { get; set; }

    }
}
