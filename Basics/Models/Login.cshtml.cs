using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic.Services;

namespace Basics.Models
{
    public class LoginModel : PageModel
    {
        private readonly IAccountServices _accountServices;
        [BindProperty]
        public LoginViewModel? Input { get; set; }

        public LoginModel(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var result = await _accountServices.LoginAsync(Input.Email, Input.Password, Input.RememberMe);
            if (result.Succeeded)
                return RedirectToPage("/Index");
            ModelState.AddModelError("", "Geçersiz giriş.");
            return Page();
        }
    }
}