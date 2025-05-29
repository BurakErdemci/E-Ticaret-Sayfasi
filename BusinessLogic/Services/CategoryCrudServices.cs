using Core.Abstract;
using Core.Abstract.IService;
using Core.Concretes.DTOs.Panel;
using Core.Concretes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public  class CategoryCrudServices:ICategoryCrudServices
    {
       private readonly IUnitOfWork unitOfWork;
        public CategoryCrudServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(string name, int? parentId, string? imagePath = null)
        {
           await this.unitOfWork.CategoryRepository.CreateAsync(new Category { 
               Name = name,
               Active = true,
               ParentId = parentId == 0 ? null : parentId,
               CoverImagePath = imagePath
           });
            await this.unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await unitOfWork.CategoryRepository.ReadByIdAsync(id);
            if (category == null) throw new Exception("Category not found");

            // Check if category has subcategories
            var categories = await unitOfWork.CategoryRepository.ReadManyAsync(c => c.ParentId == id);
            if (categories.Any())
            {
                throw new Exception("Bu kategori alt kategorilere sahip olduğu için silinemez.");
            }

            // Check if category has products
            var products = await unitOfWork.ProductRepository.ReadManyAsync(p => p.CategoryId == id);
            if (products.Any())
            {
                throw new Exception("Bu kategoriye ait ürünler olduğu için silinemez.");
            }

            await unitOfWork.CategoryRepository.DeleteAsync(category);
            await unitOfWork.CommitAsync();
        }

        public async Task<CategoryDTO?> GetAsync(int id)
        {
            var category = await unitOfWork.CategoryRepository.ReadByIdAsync(id);
            if (category == null) return null;
           
            var parent = category.ParentId.HasValue ? 
                await unitOfWork.CategoryRepository.ReadByIdAsync(category.ParentId.Value) : null;

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Active = category.Active,
                Deleted = category.Deleted,
                ParentCategory = parent != null ? new CategoryDTO 
                {
                    Id = parent.Id,
                    Name = parent.Name,
                    Active = parent.Active,
                    Deleted = parent.Deleted
                } : null,
                ImagePath = category.CoverImagePath
            };
        }

        public async Task<IEnumerable<CategoryDTO>> GetAsync()
        {
            var categories = await unitOfWork.CategoryRepository.ReadManyAsync(null);
            var dtos = new List<CategoryDTO>();

            foreach (var category in categories)
            {
                var parent = category.ParentId.HasValue ? 
                    await unitOfWork.CategoryRepository.ReadByIdAsync(category.ParentId.Value) : null;

                dtos.Add(new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Active = category.Active,
                    Deleted = category.Deleted,
                    ParentCategory = parent != null ? new CategoryDTO
                    {
                        Id = parent.Id,
                        Name = parent.Name,
                        Active = parent.Active,
                        Deleted = parent.Deleted
                    } : null,
                    ImagePath = category.CoverImagePath
                });
            }

            return dtos;
        }

        private async Task<CategoryDTO?> GetParent(int? parentId)
        {
            if(parentId == null) return null;
            var parent = await unitOfWork.CategoryRepository.ReadByIdAsync((int)parentId);
            if (parent == null) return null;
            
            return new CategoryDTO
            {
                Id = parent.Id,
                Name = parent.Name,
                Active = parent.Active,
                Deleted = parent.Deleted
            };
        }

        public async Task UpdateAsync(CategoryDTO categoryDTO)
        {
            var category = await unitOfWork.CategoryRepository.ReadByIdAsync(categoryDTO.Id);
            if (category == null) throw new Exception("Category not found");

            category.Name = categoryDTO.Name;
            category.ParentId = categoryDTO.ParentCategory?.Id;
            category.Active = categoryDTO.Active;
            category.Deleted = categoryDTO.Deleted;
            category.CoverImagePath = categoryDTO.ImagePath;

            if (category.ParentId == category.Id)
            {
                throw new Exception("Bir kategori kendisini üst kategori olarak seçemez.");
            }

            if (category.ParentId.HasValue)
            {
                var parentCategory = await unitOfWork.CategoryRepository.ReadByIdAsync(category.ParentId.Value);
                if (parentCategory == null)
                {
                    throw new Exception("Seçilen üst kategori bulunamadı.");
                }
            }

            await unitOfWork.CategoryRepository.UpdateAsync(category);
            await unitOfWork.CommitAsync();
        }
    }
}
