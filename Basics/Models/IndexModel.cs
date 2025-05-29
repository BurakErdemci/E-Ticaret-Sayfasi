using BusinessLogic.Services;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Utilities.Generics;
using Core.Concretes.DTOs.Panel;
using Core.Abstract.IService;

namespace Basics.Models
{
    public class IndexModel : PageModel
    {
        private readonly IShopServices _shopServices;
        private readonly IBrandCrudServices _brandService;
        public Pagination<ProductListItem>? Products { get; set; }
        public IEnumerable<CategoryListItem> Categories { get; set; } = [];
        public IEnumerable<BrandDTO>? Brands { get; set; }
        public IEnumerable<ProductListItem>? MostPopularProducts { get; set; }

        public IndexModel(IShopServices shopServices, IBrandCrudServices brandService)
        {
            _shopServices = shopServices;
            _brandService = brandService;
        }

        public async Task OnGetAsync([FromQuery(Name = "page")] int page = 1)
        {
            MostPopularProducts = await _shopServices.GetMostPopularProductsAsync(3);
            Products = await _shopServices.GetPaginatedProducts(page, 9);
            Categories = await _shopServices.GetAvailableCategoryAsync();
            Brands = await _brandService.GetAsync();
        }
    }
}
