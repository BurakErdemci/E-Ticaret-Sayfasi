using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic.Services;
using Core.Concretes.Entities;
using Core.Abstract.IService;

namespace Basics.Models
{
    public class CategoryModel : PageModel
    {
        private readonly ICategoryCrudServices _categoryService;

        public CategoryModel(ICategoryCrudServices categoryService)
        {
            _categoryService = categoryService;
        }

        public List<Category> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var categoryDtos = await _categoryService.GetAsync();
            Categories = categoryDtos
                .Where(dto => dto.ParentCategory == null)
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