using Core.Concretes.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Abstract.IService
{
    public interface ICartService
    {
        Task<Cart?> GetCartItemsAsync(string memberId);
        Task AddToCartAsync(string memberId, int productId, int quantity = 1);
        Task RemoveFromCartAsync(string memberId, int productId);
        Task ClearCartAsync(string memberId);
    }
} 