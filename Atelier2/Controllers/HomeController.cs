using System.Diagnostics;
using System.Linq;
using Atelier2.Models;
using Atelier2.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Atelier2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(
            ILogger<HomeController> logger,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            // Sidebar catégories
            var categories = _categoryRepository.GetAll();
            ViewData["Categories"] = categories;

            // Produits “en vedette” pour la home
            var featuredProducts = _productRepository
                .GetAll()
                .OrderBy(p => p.Name)
                .Take(8)
                .ToList();

            return View(featuredProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
