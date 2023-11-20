using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Repositories
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);

        // [Update & Create] at the same time .
        Task<CustomerBasket?> UpdateBaksetAsync(CustomerBasket customerBasket);
        Task<bool> DeleteBasketAsync(string BasketId); 
    }
}
