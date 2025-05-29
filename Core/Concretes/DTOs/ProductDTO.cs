using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concretes.DTOs
{
   public class ProductListItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public  string? CoverImagePath { get; set; }
        public  int  BrandId { get; set; }
        public  string?  BrandName { get; set; }
        public  int  CategoryId { get; set; }
        public  string? CategoryName { get; set; }
        public IEnumerable<string> Tags { get; set; } = [];
        public double Rating { get; set; }
        public int RewiewCount { get; set; }
       
        public  int LikesCount { get; set; }
        public  int  DislikesCount { get; set; }
    }

    public class ProductDetail
    {

    }
}
