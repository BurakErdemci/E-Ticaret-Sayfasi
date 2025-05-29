using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Panel.Models;

namespace Panel.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountServices services;
        public AccountController(IAccountServices services)
        {
            this.services = services;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()=> View();
            
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var result = await services.LoginAsync(model.Email, model.Password, model.RememberMe);
            if (result.Succeeded) return RedirectToAction("index", "home");
            ModelState.AddModelError("", "Başarısız Giriş Denemesi");
            return View(model);
        }
        public IActionResult Register()=> View();
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var result = await services.RegisterAsync(model.FirstName, model.LastName, model.DateOfBirth, model.Email, model.Password);
            if (result.Succeeded) return RedirectToAction("Login");
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await services.LogOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
