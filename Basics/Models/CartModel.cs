using BusinessLogic.Services;
using Core.Abstract.IService;
using Core.Concretes.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Basics.Models
{
    public class CartModel : PageModel
    {
        private readonly ICartService _cartService;

        public CartModel(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IEnumerable<CartItem>? CartItems { get; set; }
        public decimal TotalPrice { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var memberId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(memberId))
            {
                return RedirectToPage("/Login");
            }

            var cart = await _cartService.GetCartItemsAsync(memberId);
            if (cart == null || cart.Items == null || !cart.Items.Any())
            {
                CartItems = Enumerable.Empty<CartItem>();
                TotalPrice = 0;
                return Page();
            }

            CartItems = cart.Items;
            TotalPrice = cart.Items
                .Where(i => i.Product != null)
                .Sum(i => i.Product!.ListPrice * i.Quantity);
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveItemAsync(int itemId)
        {
            var memberId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(memberId))
            {
                return RedirectToPage("/Login");
            }

            var cart = await _cartService.GetCartItemsAsync(memberId);
            if (cart == null)
            {
                return NotFound();
            }

            var item = cart.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                return NotFound();
            }

            await _cartService.RemoveFromCartAsync(memberId, item.ProductId);
            return new OkResult();
        }

        public async Task<IActionResult> OnPostPurchaseAsync()
        {
            var memberId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(memberId))
            {
                return RedirectToPage("/Login");
            }

            var orderService = HttpContext.RequestServices.GetService(typeof(IOrderService)) as IOrderService;
            var order = await orderService?.CheckoutAsync(memberId)!;

            if (order == null)
            {
                return RedirectToPage("/Cart");
            }

            return RedirectToPage("/PurchaseSuccess");
        }
    }
}