using Core.Abstract;
using Core.Abstract.IService;
using Core.Concretes.DTOs.Panel;
using Core.Concretes.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class BrandCrudServices : IBrandCrudServices
    {
        private readonly IUnitOfWork unitOfWork;
        public BrandCrudServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<BrandDTO?> GetAsync(int id)
        {
            var brand = await unitOfWork.BrandRepository.ReadByIdAsync(id);
            if (brand == null) return null;
            return MapToDTO(brand);
        }

        public async Task<IEnumerable<BrandDTO>> GetAsync()
        {
            var brands = await unitOfWork.BrandRepository.ReadManyAsync(null);
            return brands.Select(MapToDTO);
        }

        public async Task CreateAsync(string name, string? description, string? logoImagePath)
        {
            var brand = new Brand
            {
                Name = name,
                Description = description,
                LogoImagePath = logoImagePath
            };
            await unitOfWork.BrandRepository.CreateAsync(brand);
            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var brand = await unitOfWork.BrandRepository.ReadByIdAsync(id);
            if (brand == null) return;
            await unitOfWork.BrandRepository.DeleteAsync(brand);
            await unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(BrandDTO dto)
        {
            var brand = await unitOfWork.BrandRepository.ReadByIdAsync(dto.Id);
            if (brand == null) return;
            brand.Name = dto.Name;
            brand.Description = dto.Description;
            brand.LogoImagePath = dto.LogoImagePath;
            await unitOfWork.BrandRepository.UpdateAsync(brand);
            await unitOfWork.CommitAsync();
        }

        private BrandDTO MapToDTO(Brand brand)
        {
            return new BrandDTO
            {
                Id = brand.Id,
                Name = brand.Name,
                Description = brand.Description,
                LogoImagePath = brand.LogoImagePath,
                Active = true,
                Deleted = false
            };
        }
    }
} 