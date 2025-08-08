using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Share.DataTransferObject.BasketModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    public class PaymentsControllar(IServiceManager _serviceManger) :BaseController
    {
        [Authorize]
        [HttpPost("{BasketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            var Basket = await _serviceManger.PaymentService.CreateOrUpdatePaymentIntentAsync(BasketId);
            return Ok(Basket);
        }
    }
}
