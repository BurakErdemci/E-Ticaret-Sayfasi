using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panel.Models;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Panel.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            var pageStatus = _config.GetSection("PageStatus").Get<Dictionary<string, bool>>();
            return View(pageStatus);
        }
    
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult TogglePageStatus(string page, bool status)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var json = System.IO.File.ReadAllText(filePath);
            dynamic config = JsonConvert.DeserializeObject(json)!;
            config["PageStatus"][page] = status;
            var updatedJson = JsonConvert.SerializeObject(config, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, updatedJson);
            return Ok();
        }
    }
}
