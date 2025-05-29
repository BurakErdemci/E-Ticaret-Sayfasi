using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Concretes.DTOs;
using Core.Abstract.IService;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Identity;

public class ProductDetailModel : PageModel
{
    private readonly IShopServices _shopServices;
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;

    public ProductListItem? Product { get; set; }

    public ProductDetailModel(IShopServices shopServices, ICartService cartService, IOrderService orderService)
    {
        _shopServices = shopServices;
        _cartService = cartService;
        _orderService = orderService;
    }

    public async Task OnGetAsync(int id)
    {
        Product = (await _shopServices.GetAvailableProductsAsync())
            .FirstOrDefault(p => p.Id == id);
    }

    public async Task<IActionResult> OnPostAsync(int productId, int quantity, string action)
    {
        var memberId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(memberId))
            return RedirectToPage("/Login");

        if (action == "addToCart")
        {
            await _cartService.AddToCartAsync(memberId, productId, quantity);
            TempData["CartMessage"] = "Ürün sepete eklendi!";
            return RedirectToPage(new { id = productId });
        }
        else if (action == "buyNow")
        {
            await _cartService.AddToCartAsync(memberId, productId, quantity);
            await _orderService.CheckoutAsync(memberId);
            TempData["OrderMessage"] = "Satın alma işlemi başarılı!";
            return RedirectToPage("/Orders");
        }
        return RedirectToPage(new { id = productId });
    }
} 