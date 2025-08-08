using Domain.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class OrderWhithPaymentIntentIdSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderWhithPaymentIntentIdSpecifications(string PaymentIntetnId) : base(O => O.PaymentIntentId == PaymentIntetnId)
        {

        }
    }
}
