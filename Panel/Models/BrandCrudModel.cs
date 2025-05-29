using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Panel.Models
{
    public class BrandCreateModel
    {
        [Required]
        [Display(Name = "Brand Name", Prompt = "Brand Name")]
        [StringLength(100)]
        public required string Name { get; set; }

        [Display(Name = "Description", Prompt = "Brand Description")]
        public string? Description { get; set; }

        [Display(Name = "Logo Image")]
        public IFormFile? LogoImage { get; set; }
    }

    public class BrandEditModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Brand Name", Prompt = "Brand Name")]
        [StringLength(100)]
        public required string Name { get; set; }

        [Display(Name = "Description", Prompt = "Brand Description")]
        public string? Description { get; set; }

        [Display(Name = "Logo Image")]
        public IFormFile? LogoImage { get; set; }

        public string? CurrentLogoPath { get; set; }
        public bool Active { get; set; }
    }
} 