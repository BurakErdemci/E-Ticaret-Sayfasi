using Core.Abstract.IService;
using Microsoft.AspNetCore.Mvc;
using Panel.Models;
using Core.Concretes.DTOs.Panel;
using System.Threading.Tasks;
using System.IO;
using System;

namespace Panel.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandCrudServices brandService;
        public BrandController(IBrandCrudServices brandService)
        {
            this.brandService = brandService;
        }

        // GET: Brand
        public async Task<IActionResult> Index()
        {
            var brands = await brandService.GetAsync();
            return View(brands);
        }

        // GET: Brand/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brand/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string? logoPath = null;
            if (model.LogoImage != null && model.LogoImage.Length > 0)
            {
                var fileName = $"brand_{Guid.NewGuid()}{System.IO.Path.GetExtension(model.LogoImage.FileName)}";
                var savePath = Path.Combine("wwwroot", "uploads", "brands", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await model.LogoImage.CopyToAsync(stream);
                }
                logoPath = $"/uploads/brands/{fileName}";
            }
            await brandService.CreateAsync(model.Name, model.Description, logoPath);
            return RedirectToAction(nameof(Index));
        }

        // GET: Brand/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await brandService.GetAsync(id);
            if (brand == null) return NotFound();

            var model = new BrandEditModel
            {
                Id = brand.Id,
                Name = brand.Name,
                Description = brand.Description,
                CurrentLogoPath = brand.LogoImagePath,
                Active = brand.Active
            };
            return View(model);
        }

        // POST: Brand/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BrandEditModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string? logoPath = model.CurrentLogoPath;
            if (model.LogoImage != null && model.LogoImage.Length > 0)
            {
                var fileName = $"brand_{Guid.NewGuid()}{System.IO.Path.GetExtension(model.LogoImage.FileName)}";
                var savePath = Path.Combine("wwwroot", "uploads", "brands", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await model.LogoImage.CopyToAsync(stream);
                }
                logoPath = $"/uploads/brands/{fileName}";
            }

            var dto = new BrandDTO
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                LogoImagePath = logoPath,
                Active = model.Active
            };
            await brandService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // POST: Brand/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await brandService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Brand/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var brand = await brandService.GetAsync(id);
            if (brand == null) return NotFound();
            return View(brand);
        }
    }
} 