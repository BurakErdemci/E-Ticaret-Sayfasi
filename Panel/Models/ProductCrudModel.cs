using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Panel.Models
{
    public class ProductCreateModel
    {
        [Required]
        [Display(Name = "Ürün Adı", Prompt = "Ürün Adı")]
        [StringLength(100)]
        public required string Name { get; set; }

        [Display(Name = "Açıklama", Prompt = "Ürün Açıklaması")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Kategori", Prompt = "Kategori Seçin")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Marka", Prompt = "Marka Seçin")]
        public int BrandId { get; set; }

        [Required]
        [Display(Name = "Fiyat", Prompt = "Ürün Fiyatı")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır.")]
        public decimal Price { get; set; }

        [Display(Name = "Ürün Resmi")]
        public IFormFile? Image { get; set; }

        [Display(Name = "Aktif mi?")]
        public bool Active { get; set; } = true;
    }

    public class ProductEditModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ürün Adı", Prompt = "Ürün Adı")]
        [StringLength(100)]
        public required string Name { get; set; }

        [Display(Name = "Açıklama", Prompt = "Ürün Açıklaması")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Kategori", Prompt = "Kategori Seçin")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Marka", Prompt = "Marka Seçin")]
        public int BrandId { get; set; }

        [Required]
        [Display(Name = "Fiyat", Prompt = "Ürün Fiyatı")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır.")]
        public decimal Price { get; set; }

        [Display(Name = "Ürün Resmi")]
        public IFormFile? Image { get; set; }

        public string? CurrentImagePath { get; set; }
        public bool Active { get; set; }
    }
} 