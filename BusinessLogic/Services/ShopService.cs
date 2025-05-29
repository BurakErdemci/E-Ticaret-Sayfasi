using Core.Abstract;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Generics;
using Utilities.Helpers;

namespace BusinessLogic.Services
{
    public class ShopService:IShopServices
    {
        private readonly IUnitOfWork uow;

        public ShopService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IEnumerable<BrandListItem>> GetAvailableBrandsAsync()
        {
            var data = await uow.BrandRepository.ReadManyAsync(x => x.Active && !x.Deleted);
            return from b in data
                   select new BrandListItem
                   {Id=b.Id,
                   Name=b.Name

                   };
        }

        public async Task<IEnumerable<CategoryListItem>> GetAvailableCategoryAsync()
        {
           var data= await uow.CategoryRepository.ReadManyAsync(x=>x.Active && !x.Deleted);
            return from c in data
                   select new CategoryListItem
                   {
                     Id=c.Id,
                     Name=c.Name

                   };
        }

        public async Task<IEnumerable<ProductListItem>> GetAvailableProductsAsync()
        {
            var data = await uow.ProductRepository.ReadManyAsync(x => x.Active && !x.Deleted, "Brand", "Category", "Reactions", "Rewiews", "Tags");
            var list = data.Select(p => new ProductListItem
            {
                Id = p.Id,
                Name = p.Name,
                BrandId = p.BrandId,
                CategoryId = p.CategoryId,
                BrandName = p.Brand?.Name,
                CategoryName = p.Category?.Name,
                Price = p.ListPrice * (100 + p.TaxRate) / 100,
                DiscountedPrice = p.ListPrice * (100 - p.DiscountRate) * (100 + p.TaxRate) / 10000,
                LikesCount = p.Reactions.Count != 0 ? p.Reactions.Count(x => x.ReactionType == ReactionType.LIKE) : 0,
                DislikesCount = p.Reactions.Count != 0 ? p.Reactions.Count(x => x.ReactionType == ReactionType.DISLIKE) : 0,
                Rating = p.Rewiews.Count != 0 ? p.Rewiews.Average(x => x.Star) : 0,
                Tags = p.Tags.Select(x => x.Name).ToList(),
                RewiewCount = p.Rewiews.Count,
                CoverImagePath=p.ImagePath
            }).ToList();


            return list;
        }

        public async Task<Pagination<ProductListItem>> GetPaginatedProducts(int page , int limit)
        {
            var data = await GetAvailableProductsAsync();
            return new Pagination<ProductListItem>(data, page, limit);
        }
        public async Task<IEnumerable<ProductListItem>> GetMostPopularProductsAsync(int count = 3)
        {
            var data = await uow.ProductRepository.ReadManyAsync(x => x.Active && !x.Deleted, "Brand", "Category", "OrderItems", "Reactions", "Rewiews", "Tags");
            var list = data
                .OrderByDescending(p => p.OrderItems.Count)
                .Take(count)
                .Select(p => new ProductListItem
                {
                    Id = p.Id,
                    Name = p.Name,
                    BrandId = p.BrandId,
                    CategoryId = p.CategoryId,
                    BrandName = p.Brand?.Name,
                    CategoryName = p.Category?.Name,
                    Price = p.ListPrice * (100 + p.TaxRate) / 100,
                    DiscountedPrice = p.ListPrice * (100 - p.DiscountRate) * (100 + p.TaxRate) / 10000,
                    LikesCount = p.Reactions.Count != 0 ? p.Reactions.Count(x => x.ReactionType == ReactionType.LIKE) : 0,
                    DislikesCount = p.Reactions.Count != 0 ? p.Reactions.Count(x => x.ReactionType == ReactionType.DISLIKE) : 0,
                    Rating = p.Rewiews.Count != 0 ? p.Rewiews.Average(x => x.Star) : 0,
                    Tags = p.Tags.Select(x => x.Name).ToList(),
                    RewiewCount = p.Rewiews.Count,
                    CoverImagePath = p.ImagePath
                }).ToList();
            return list;
        }
        private async Task<string?> GetCoverImagePath(int pid)
        {
            var image = await uow.MediaGalleryRepository.ReadFirstAsync(x => x.ProductId == pid);
            return image?.MediaPath;
        }
    }
    
}
