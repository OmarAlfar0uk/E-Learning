using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Share.DataTransferObject.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    public class OrdersControllar(IServiceManager _serviceManger) : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTo>> CreateOrderAsync(OrderDTo orderDTo)
        {

            var Order = await _serviceManger.OrederService.CreateOrderAsync(orderDTo, GetEmailFromToken());
            return Ok(Order);
        }
        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDTo>>> GetDeliveryMethods()
        {
            var DeliveryMethods = await _serviceManger.OrederService.GetDeliveryMethodAsync();
            return Ok(DeliveryMethods);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTo>>> GetAllOrders()
        {
            var Order = await _serviceManger.OrederService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(Order);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDTo>> GetOrderById(Guid id)
        {
            var Order = await _serviceManger.OrederService.GetOrderByIdAsync(id);
            return Ok(Order);
        }
    }
}
