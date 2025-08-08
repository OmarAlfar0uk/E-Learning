using Domain.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.OrderModuleSpecification
{
    internal class OrderSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderSpecifications(string Email) : base(O => O.buyerEmail == Email)
        {
            AddInclode(O => O.DeliveryMethod);
            AddInclode(O => O.Items);
            AddOrderByDescending(O => O.OrderDate);
        }

        public OrderSpecifications(Guid Id) : base(O => O.Id == Id)
        {
            AddInclode(O => O.DeliveryMethodId);
            AddInclode(O => O.Items);
        }
    }
}
