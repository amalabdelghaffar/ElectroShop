using Atelier2.Models.Help;
using Microsoft.EntityFrameworkCore;

namespace Atelier2.Models.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly AppDbContext context;

        public OrderRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Add(Order o)
        {
            // Validation de l'objet Order avant l'ajout
            if (o == null)
                throw new ArgumentNullException(nameof(o), "L'objet Order ne peut pas être null");

            // Validation des champs obligatoires
            if (string.IsNullOrWhiteSpace(o.CustomerName))
                throw new ArgumentException("Le nom du client est obligatoire");

            if (string.IsNullOrWhiteSpace(o.Address))
                throw new ArgumentException("L'adresse de livraison est obligatoire");

            if (o.TotalAmount <= 0)
                throw new ArgumentException("Le montant total doit être supérieur à 0");

            // Définir la date de commande si elle n'est pas définie
            if (o.OrderDate == default(DateTime))
                o.OrderDate = DateTime.Now;

            // Validation des items
            if (o.Items == null || !o.Items.Any())
                throw new ArgumentException("La commande doit contenir au moins un article");

            try
            {
                context.Orders.Add(o);
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Log de l'erreur pour le débogage avec plus de détails
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"Erreur lors de la sauvegarde de la commande: {innerMessage}", ex);
            }
        }

        public Order GetById(int id)
        {
            return context.Orders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == id);
        }
    }
}