using Atelier2.Models.Repositories;

namespace Atelier2.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public IList<Category> GetAll()
        {
            return _context.Categories
                .OrderBy(c => c.CategoryName)
                .ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.Find(id);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }


        public Category Update(Category category)
        {
            var existing = _context.Categories.Find(category.CategoryId);
            if (existing != null)
            {
                existing.CategoryName = category.CategoryName;
                _context.SaveChanges();
            }
            return existing;
        }

        public void Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}
