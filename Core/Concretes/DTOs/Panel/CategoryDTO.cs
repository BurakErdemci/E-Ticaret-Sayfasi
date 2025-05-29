using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concretes.DTOs.Panel
{
    public class CategoryDTO
    {
        public  int  Id { get; set; }
        public  string Name { get; set; }    
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public CategoryDTO? ParentCategory { get; set; }
        public string? ImagePath { get; set; }
    }
}
