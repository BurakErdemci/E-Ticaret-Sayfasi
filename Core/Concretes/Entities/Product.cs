using Core.Abstract.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concretes.Entities
{
    [Table("products",Schema ="production")]
    public class Product:BaseEntity
    {
        [Required]
        public required  string Name { get; set; }
        public  string? Description { get; set; }
        public DateTime? ExpirationDate { get; set; } = null;
        public  string? Origin { get; set; }
        public int MakeFlag { get; set; } = 0;
        [Required]
        public decimal ListPrice { get; set; }
        [Required]
        
        public decimal DiscountRate { get; set; } = 0;
        [Required]
        public decimal TaxRate { get; set; }
        [Required]
        public int CoverImageId { get; set; }
        public virtual ICollection<ProductMedia> MediaLibrary { get; set; } = [];

        [Required]
        public  int CategoryId { get; set; }
        [Required]
        public int BrandId { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = [];
        public  virtual ICollection<Tags> Tags { get; set; } = [];
        public virtual ICollection<OrderDetail> OrderItems { get; set; } = [];
        public virtual ICollection<Reaction> Reactions { get; set; } = [];
        public virtual ICollection<Rewiew> Rewiews { get; set; } = [];
        public string? ImagePath { get; set; }





    }
}
