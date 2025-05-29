using Core.Concretes.DTOs.Panel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Abstract.IService
{
    public interface IBrandCrudServices
    {
        Task<BrandDTO?> GetAsync(int id);
        Task<IEnumerable<BrandDTO>> GetAsync();
        Task CreateAsync(string name, string? description, string? logoImagePath);
        Task DeleteAsync(int id);
        Task UpdateAsync(BrandDTO brandDTO);
    }
} 