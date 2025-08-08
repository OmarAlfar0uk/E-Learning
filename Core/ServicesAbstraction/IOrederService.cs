using Share.DataTransferObject.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IOrederService
    {
        Task<OrderToReturnDTo> CreateOrderAsync(OrderDTo orderDTo, string Email);

        Task<IEnumerable<DeliveryMethodDTo>> GetDeliveryMethodAsync();

        Task<IEnumerable<OrderToReturnDTo>> GetAllOrdersAsync(string Email);

        Task<OrderToReturnDTo> GetOrderByIdAsync(Guid Id);

    }
}
