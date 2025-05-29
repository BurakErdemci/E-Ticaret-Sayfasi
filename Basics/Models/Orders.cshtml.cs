using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Abstract.IService;
using Core.Concretes.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Basics.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly IOrderService _orderService;

        public OrdersModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IEnumerable<OrderHeader>? Orders { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var memberId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(memberId))
            {
                return RedirectToPage("/Login");
            }

            Orders = await _orderService.GetOrdersAsync(memberId);
            return Page();
        }
    }
} 