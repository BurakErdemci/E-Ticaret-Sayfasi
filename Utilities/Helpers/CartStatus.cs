using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Helpers
{
    public enum CartStatus
    {
        ACTIVE,
        ABONDENED,
        COMPLETED,
        EXPIRED,

       

    }
    public enum OrderStatus
    {
        PENDING,
        PROCESSING,
        SHIPPED,
        DELIVERED,
        CANCELED,
        REFUNDED
            
    }
}
