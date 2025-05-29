using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic.Services;
using Core.Concretes.Entities;
using Core.Abstract.IService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basics.Models
{
    public class CategoryDetailModel : PageModel
    {
        private readonly ICategoryCrudServices _categoryService;
        public Category ParentCategory { get; set; }
        public List<Category> SubCategories { get; set; }

        public CategoryDetailModel(ICategoryCrudServices categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var allDtos = await _categoryService.GetAsync();
            var parent = allDtos.FirstOrDefault(x => x.Id == id);
            if (parent == null) return NotFound();

            ParentCategory = new Category
            {
                Id = parent.Id,
                Name = parent.Name,
                CoverImagePath = parent.ImagePath
            };

            SubCategories = allDtos
                .Where(c => c.ParentCategory != null && c.ParentCategory.Id == id)
                .Select(dto => new Category
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    CoverImagePath = dto.ImagePath
                }).ToList();

            return Page();
        }
    }
}