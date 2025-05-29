using Core.Concretes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Generics;

namespace BusinessLogic.Services
{
    public interface IShopServices
    {
     

        Task<IEnumerable<ProductListItem>> GetAvailableProductsAsync();
        Task<IEnumerable<CategoryListItem>> GetAvailableCategoryAsync();
        Task<IEnumerable<BrandListItem>> GetAvailableBrandsAsync();
        Task<Pagination<ProductListItem>> GetPaginatedProducts(int page, int limit);
        Task<IEnumerable<ProductListItem>> GetMostPopularProductsAsync(int count = 3);

    }
}
