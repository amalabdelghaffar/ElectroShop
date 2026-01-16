namespace Atelier2.Models.Repositories
{
    public interface ICategoryRepository
    {
        Category GetById(int id);
        IList<Category> GetAll();
        void Add(Category category);
        Category Update(Category category);
        void Delete(int id);
    }
}
