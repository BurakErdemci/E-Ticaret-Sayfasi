using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Concretes.DTOs.Panel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class BrandDetailModel : PageModel
{
    public BrandDTO? Brand { get; set; }

    public async Task OnGetAsync(int id)
    {
        // MOCK DATA (gerçek projede servisten çekilecek)
        var brands = new List<BrandDTO>
        {
            new BrandDTO { Id = 1, Name = "Ercan Burger", Description = "Kalitenin Bir Numaralı Adresi", LogoImagePath = "/assets/img/brands/ercanburger.png" },
            new BrandDTO { Id = 2, Name = "Sandfall Interactive", Description = "Fransız oyun geliştirici", LogoImagePath = "/assets/img/brands/sandfall.png" },
            new BrandDTO { Id = 3, Name = "Dominos", Description = "Pizzanın bir numaralı adresi!", LogoImagePath = "/assets/img/brands/dominos.png" },
            new BrandDTO { Id = 4, Name = "Defacto", Description = "Defacto", LogoImagePath = "/assets/img/brands/defacto.png" },
            new BrandDTO { Id = 5, Name = "RockstarGames", Description = "RockstarGames", LogoImagePath = "/assets/img/brands/rockstar.png" },
            new BrandDTO { Id = 6, Name = "Bosch", Description = "Bosch", LogoImagePath = "/assets/img/brands/bosch.png" }
        };
        Brand = brands.FirstOrDefault(b => b.Id == id);
    }
} 