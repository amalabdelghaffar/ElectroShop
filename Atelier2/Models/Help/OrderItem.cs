namespace Atelier2.Models.Help
{
    public class OrderItem
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        // Clé étrangère vers Order
        public int OrderId { get; set; }

        // Navigation property vers Order (CORRECT)
        public Order Order { get; set; }
    }
}