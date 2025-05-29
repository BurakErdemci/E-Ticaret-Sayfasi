using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concretes.DTOs.Panel
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImagePath { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public CategoryDTO? Category { get; set; }
        public int BrandId { get; set; }
        public BrandDTO? Brand { get; set; }
    }
} 