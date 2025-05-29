using Core.Abstract.IService;
using Microsoft.AspNetCore.Mvc;
using Panel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Linq;
using Core.Concretes.DTOs.Panel;
using System.IO;
using System;

namespace Panel.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductCrudServices productService;
        private readonly ICategoryCrudServices categoryService;
        private readonly IBrandCrudServices brandService;
        public ProductController(IProductCrudServices productService, ICategoryCrudServices categoryService, IBrandCrudServices brandService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.brandService = brandService;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAsync();
            return View(products);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            var categories = await categoryService.GetAsync();
            ViewData["Categories"] = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            var brands = await brandService.GetAsync();
            ViewData["Brands"] = brands.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            }).ToList();

            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var categories = await categoryService.GetAsync();
                    ViewData["Categories"] = categories.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();
                    return View(model);
                }
                string? imagePath = null;
                if (model.Image != null && model.Image.Length > 0)
                {
                    var fileName = $"product_{Guid.NewGuid()}{System.IO.Path.GetExtension(model.Image.FileName)}";
                    var savePath = Path.Combine("wwwroot", "uploads", "products", fileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }
                    imagePath = $"/uploads/products/{fileName}";
                }
                await productService.CreateAsync(model.Name, model.Description, model.CategoryId, model.Price, imagePath, model.Active, model.BrandId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ürün eklenirken bir hata oluştu: " + ex.Message);
                var categories = await categoryService.GetAsync();
                ViewData["Categories"] = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
                return View(model);
            }
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await productService.GetAsync(id);
            if (product == null) return NotFound();

            var categories = await categoryService.GetAsync();
            ViewData["Categories"] = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            var brands = await brandService.GetAsync();
            ViewData["Brands"] = brands.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            }).ToList();

            var model = new ProductEditModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.Category?.Id ?? 0,
                BrandId = product.BrandId,
                Price = product.Price,
                Active = product.Active,
                CurrentImagePath = product.ImagePath
            };
            return View(model);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                var categories = await categoryService.GetAsync();
                ViewData["Categories"] = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
                var brands = await brandService.GetAsync();
                ViewData["Brands"] = brands.Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList();
                return View(model);
            }

            string? imagePath = model.CurrentImagePath;
            if (model.Image != null && model.Image.Length > 0)
            {
                var fileName = $"product_{Guid.NewGuid()}{System.IO.Path.GetExtension(model.Image.FileName)}";
                var savePath = Path.Combine("wwwroot", "uploads", "products", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
                imagePath = $"/uploads/products/{fileName}";
            }

            var dto = new ProductDTO
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Category = new CategoryDTO { Id = model.CategoryId },
                BrandId = model.BrandId,
                Active = model.Active,
                ImagePath = imagePath
            };
            await productService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await productService.GetAsync(id);
            if (product == null) return NotFound();
            if (product.BrandId > 0)
            {
                var brand = await brandService.GetAsync(product.BrandId);
                product.Brand = brand;
            }
            return View(product);
        }
    }
} 