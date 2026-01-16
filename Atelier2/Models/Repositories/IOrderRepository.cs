using Atelier2.Models.Help;

namespace Atelier2.Models.Repositories
{
    public interface IOrderRepository
    {
        Order GetById(int Id);
        void Add(Order o);
    }
}
