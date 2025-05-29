using Core.Concretes.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Abstract.IService
{
    public interface IOrderService
    {
        Task<OrderHeader?> CheckoutAsync(string memberId);
        Task<IEnumerable<OrderHeader>> GetOrdersAsync(string memberId);
    }
} 