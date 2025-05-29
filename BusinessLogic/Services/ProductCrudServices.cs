using Core.Abstract;
using Core.Abstract.IService;
using Core.Concretes.DTOs.Panel;
using Core.Concretes.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProductCrudServices : IProductCrudServices
    {
        private readonly IUnitOfWork unitOfWork;
        public ProductCrudServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ProductDTO?> GetAsync(int id)
        {
            var products = await unitOfWork.ProductRepository.ReadManyAsync(p => p.Id == id, "Category", "Brand");
            var product = products.FirstOrDefault();
            if (product == null) return null;
            return MapToDTO(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetAsync()
        {
            var products = await unitOfWork.ProductRepository.ReadManyAsync(null, "Category", "Brand");
            return products.Select(MapToDTO);
        }

        public async Task CreateAsync(string name, string? description, int categoryId, decimal price, string? imagePath, bool active, int brandId)
        {
            var product = new Product
            {
                Name = name,
                Description = description,
                CategoryId = categoryId,
                BrandId = brandId,
                ListPrice = price,
                ImagePath = imagePath,
                Active = active,
                Deleted = false
            };
            await unitOfWork.ProductRepository.CreateAsync(product);
            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await unitOfWork.ProductRepository.ReadByIdAsync(id);
            if (product == null) return;
            await unitOfWork.ProductRepository.DeleteAsync(product);
            await unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(ProductDTO dto)
        {
            var product = await unitOfWork.ProductRepository.ReadByIdAsync(dto.Id);
            if (product == null) return;
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.CategoryId = dto.Category?.Id ?? 0;
            product.BrandId = dto.BrandId;
            product.ListPrice = dto.Price;
            product.ImagePath = dto.ImagePath;
            product.Active = dto.Active;
            await unitOfWork.ProductRepository.UpdateAsync(product);
            await unitOfWork.CommitAsync();
        }

        private ProductDTO MapToDTO(Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.ListPrice,
                ImagePath = product.ImagePath,
                Category = product.Category != null ? new CategoryDTO
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name,
                    Active = true,
                    Deleted = false,
                    ImagePath = product.Category.CoverImagePath
                } : null,
                BrandId = product.BrandId,
                Brand = product.Brand != null ? new BrandDTO
                {
                    Id = product.Brand.Id,
                    Name = product.Brand.Name,
                    Description = product.Brand.Description,
                    LogoImagePath = product.Brand.LogoImagePath,
                    Active = true,
                    Deleted = false
                } : null,
                Active = product.Active
            };
        }
    }
} 