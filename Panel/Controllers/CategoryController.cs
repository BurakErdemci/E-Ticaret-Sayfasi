using BusinessLogic.Services;
using Core.Abstract.IService;
using Core.Concretes.DTOs.Panel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Panel.Models;

namespace Panel.Controllers
{
    public class CategoryController : Controller
    {
        // GET: CategoryController
        private  readonly   ICategoryCrudServices services;
        public CategoryController(ICategoryCrudServices services)
        {
            this.services = services;
        }
        public async Task<ActionResult> Index()
        {   
            return View(await services.GetAsync());
        }

        // GET: CategoryController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await services.GetAsync(id));
        }

        // GET: CategoryController/Create
        public async Task<ActionResult> Create()

        {
            var categories = await services.GetAsync();
            ViewData["Categories"] = from c in categories
                                     where c.ParentCategory == null
                                     select new SelectListItem
                                     {
                                         Value = c.Id.ToString(),
                                         Text = c.Name,
                                     };
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryCreateModel model)
        {
            var categories = await services.GetAsync();
            ViewData["Categories"] = from c in categories
                                     where c.ParentCategory == null
                                     select new SelectListItem
                                     {
                                         Value = c.Id.ToString(),
                                         Text = c.Name,
                                     };
            try
            {
                if(!ModelState.IsValid) return View(model);
                string? imagePath = null;
                if (model.Image != null && model.Image.Length > 0)
                {
                    var fileName = $"category_{Guid.NewGuid()}{System.IO.Path.GetExtension(model.Image.FileName)}";
                    var savePath = Path.Combine("wwwroot", "uploads", "categories", fileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }
                    imagePath = $"/uploads/categories/{fileName}";
                }
                await services.CreateAsync(model.Name, model.ParentId, imagePath);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var category = await services.GetAsync(id);
            if (category == null) return NotFound();

            var categories = await services.GetAsync();
            ViewData["Categories"] = from c in categories
                                      where c.Id != id && c.ParentCategory == null
                                   select new SelectListItem
                                   {
                                       Value = c.Id.ToString(),
                                       Text = c.Name,
                                   };

            return View(new CategoryEditModel 
            { 
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentCategory?.Id ?? 0,
                Active = category.Active,
                CurrentImagePath = category.ImagePath
            });
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CategoryEditModel model)
        {
            if (id != model.Id) return NotFound();

            var categories = await services.GetAsync();
            ViewData["Categories"] = from c in categories
                                      where c.Id != id && c.ParentCategory == null
                                   select new SelectListItem
                                   {
                                       Value = c.Id.ToString(),
                                       Text = c.Name,
                                   };

            try
            {
                if (!ModelState.IsValid) return View(model);

                // Mevcut kategoriyi al
                var existingCategory = await services.GetAsync(id);
                if (existingCategory == null) return NotFound();

                string? imagePath = existingCategory.ImagePath;
                if (model.Image != null && model.Image.Length > 0)
                {
                    var fileName = $"category_{Guid.NewGuid()}{System.IO.Path.GetExtension(model.Image.FileName)}";
                    var savePath = Path.Combine("wwwroot", "uploads", "categories", fileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }
                    imagePath = $"/uploads/categories/{fileName}";
                }

                var categoryDTO = new CategoryDTO
                {
                    Id = model.Id,
                    Name = model.Name,
                    Active = model.Active,
                    Deleted = existingCategory.Deleted,
                    ParentCategory = model.ParentId == 0 ? null : new CategoryDTO { Id = model.ParentId },
                    ImagePath = imagePath
                };

                await services.UpdateAsync(categoryDTO);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu: " + ex.Message);
                return View(model);
            }
        }

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await services.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Edit), new { id });
            }
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await services.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Edit), new { id });
            }
        }
    }
}
