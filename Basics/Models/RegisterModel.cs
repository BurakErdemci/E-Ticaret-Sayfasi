using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic.Services;

namespace Basics.Models
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountServices _accountServices;
        [BindProperty]
        public RegisterViewModel Input { get; set; }

        public RegisterModel(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var result = await _accountServices.RegisterAsync(Input.FirstName, Input.LastName, Input.DateOfBirth, Input.Email, Input.Password);
            if (result.Succeeded)
                return RedirectToPage("/Login");
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
            return Page();
        }
    }
}