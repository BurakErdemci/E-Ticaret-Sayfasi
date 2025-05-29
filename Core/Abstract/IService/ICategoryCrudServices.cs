using Core.Concretes.DTOs.Panel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract.IService
{
    public interface ICategoryCrudServices
    {
        Task<CategoryDTO?> GetAsync(int id);
        Task<IEnumerable<CategoryDTO>> GetAsync();
        Task CreateAsync(string name, int? parentId, string? imagePath = null);
        Task DeleteAsync(int id);
        Task UpdateAsync(CategoryDTO categoryDTO);
    }
}
