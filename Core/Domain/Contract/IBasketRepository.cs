using Domain.Models.BasketModuel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract
{
  
        public interface IBasketRepository
        {
            Task<CustomerBasket?> GetBasketAsync(string Key);

            Task<CustomerBasket?> CreateOrUpdatBaskAsync(CustomerBasket basket, TimeSpan? TimeToLive = null);

            Task<bool> DeletBasketAsync(string id);
         }
    
}
