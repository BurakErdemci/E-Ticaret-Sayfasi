using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Concretes.DTOs.Panel;
using Core.Abstract.IService;

public class BrandModel : PageModel
{
    private readonly IBrandCrudServices _brandService;
    public IEnumerable<BrandDTO>? Brands { get; set; }

    public BrandModel(IBrandCrudServices brandService)
    {
        _brandService = brandService;
    }

    public async Task OnGetAsync()
    {
        Brands = await _brandService.GetAsync();
    }
} 