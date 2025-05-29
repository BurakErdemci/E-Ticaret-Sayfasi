using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Concretes.DTOs;
using BusinessLogic.Services;

public class ProductModel : PageModel
{
    private readonly IShopServices _shopServices;
    public IEnumerable<ProductListItem>? Products { get; set; }

    public ProductModel(IShopServices shopServices)
    {
        _shopServices = shopServices;
    }

    public async Task OnGetAsync(int? categoryId)
    {
        var allProducts = await _shopServices.GetAvailableProductsAsync();
        if (categoryId.HasValue)
            Products = allProducts.Where(p => p.CategoryId == categoryId.Value).ToList();
        else
            Products = allProducts;
    }
} 