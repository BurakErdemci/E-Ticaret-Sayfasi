using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Panel.Models
{
    public class CategoryCreateModel

    {
        [Required]
        [Display(Name ="Kategori Adı" , Prompt ="Kategori Adı")]
        [StringLength(100)]
       
        public required string Name { get; set; }
        [Display(Name="Üst Kategori",Prompt ="Üst Kategori")]
        public int ParentId { get; set; }
        public IFormFile? Image { get; set; }
        public string? CurrentImagePath { get; set; }
    }
    public class CategoryEditModel

    {
        public  int Id { get; set; }

        [Required]
    
        [Display(Name = "Kategori Adı", Prompt = "Kategori Adı")]
        [StringLength(100)]
        public required string Name { get; set; }
        [Display(Name = "Üst Kategori", Prompt = "Üst Kategori")]
        public int ParentId { get; set; }
        public bool Active { get; set; }
        public IFormFile? Image { get; set; }
        public string? CurrentImagePath { get; set; }
    }
}
