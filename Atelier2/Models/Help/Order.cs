using Microsoft.AspNetCore.Identity;

namespace Atelier2.Models.Help
{
    public class Order
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; }

        // Id de l'utilisateur connecté (si tu utilises Identity)
        public string UserId { get; set; }

        // Liste des lignes de commande (facultatif mais pratique)
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}