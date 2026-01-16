using Atelier2.Models;
using Atelier2.Models.Repositories;
using Atelier2.ViewModel;
using Atelier2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;

namespace Atelier2.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class ProductController : Controller
    {
        readonly IProductRepository ProductRepository;
        readonly ICategoryRepository CategRepository;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ProductController(IProductRepository prodRepository, ICategoryRepository categRepository, IWebHostEnvironment hostingEnvironment)
        {
            ProductRepository = prodRepository;
            CategRepository = categRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        // ✅ GET: ProductController
        [AllowAnonymous]
        public IActionResult Index(int? categoryId, int page = 1)
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            int pageSize = 4; // Nombre de produits par page
            IQueryable<Product> productsQuery = ProductRepository.GetAll().AsQueryable();

            if (categoryId.HasValue)
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);

            var totalProducts = productsQuery.Count();
            var products = productsQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.CategoryId = categoryId;

            return View(products);
        }

        // ✅ GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            var product = ProductRepository.GetById(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        // ✅ GET: ProductController/Create
        public ActionResult Create()
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            ViewBag.CategoryId = new SelectList(CategRepository.GetAll(), "CategoryId", "CategoryName");
            return View(new CreateViewModel());
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.ImagePath != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.ImagePath.CopyTo(fileStream);
                    }
                }

                Product newProduct = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    QteStock = model.QteStock,
                    CategoryId = model.CategoryId,
                    Image = uniqueFileName
                };

                ProductRepository.Add(newProduct);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(CategRepository.GetAll(), "CategoryId", "CategoryName");
            return View(model);
        }

        // ✅ GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            ViewBag.CategoryId = new SelectList(CategRepository.GetAll(), "CategoryId", "CategoryName");
            Product product = ProductRepository.GetById(id);

            EditViewModel productEditViewModel = new EditViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                QteStock = product.QteStock,
                CategoryId = product.CategoryId,
                ExistingImagePath = product.Image
            };

            return View(productEditViewModel);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            ViewBag.CategoryId = new SelectList(CategRepository.GetAll(), "CategoryId", "CategoryName");

            if (ModelState.IsValid)
            {
                Product product = ProductRepository.GetById(model.ProductId);

                product.Name = model.Name;
                product.Price = model.Price;
                product.QteStock = model.QteStock;
                product.CategoryId = model.CategoryId;

                if (model.ImagePath != null)
                {
                    if (model.ExistingImagePath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingImagePath);
                        System.IO.File.Delete(filePath);
                    }

                    product.Image = ProcessUploadedFile(model);
                }

                Product updatedProduct = ProductRepository.Update(product);

                if (updatedProduct != null)
                    return RedirectToAction("Index");
                else
                    return NotFound();
            }

            return View(model);
        }

        [NonAction]
        private string ProcessUploadedFile(EditViewModel model)
        {
            string uniqueFileName = null;

            if (model.ImagePath != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImagePath.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        // ✅ GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            var product = ProductRepository.GetById(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Search(string val)
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            var result = ProductRepository.FindByName(val);
            return View("Index", result);
        }
    }
}
