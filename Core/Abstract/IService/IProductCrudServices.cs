using Core.Concretes.DTOs.Panel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract.IService
{
    public interface IProductCrudServices
    {
        Task<ProductDTO?> GetAsync(int id);
        Task<IEnumerable<ProductDTO>> GetAsync();
        Task CreateAsync(string name, string? description, int categoryId, decimal price, string? imagePath, bool active, int brandId);
        Task DeleteAsync(int id);
        Task UpdateAsync(ProductDTO productDTO);
    }
} 