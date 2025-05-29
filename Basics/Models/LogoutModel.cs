using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic.Services;

namespace Basics.Models
{
    public class LogoutModel : PageModel
    {
        private readonly IAccountServices _accountServices;
        public LogoutModel(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }
        public async Task<IActionResult> OnPost()
        {
            await _accountServices.LogOutAsync();
            return RedirectToPage("/Index");
        }
        public IActionResult OnGet()
        {
            return RedirectToPage("/Index");
        }
    }
}