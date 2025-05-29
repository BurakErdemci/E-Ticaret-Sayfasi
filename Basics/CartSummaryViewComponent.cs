using Microsoft.AspNetCore.Mvc;
using Core.Abstract.IService;
using System.Security.Claims;

namespace WebUI.ViewComponents
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartSummaryViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var cart = await _cartService.GetCartItemsAsync(userId);
                int count = cart?.Items?.Count ?? 0;
                return View("~/Views/Shared/Default.cshtml", count);
            }

            return View("~/Views/Shared/Default.cshtml", 0); // Giriş yapılmamışsa
        }
    }
}
