using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModule
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {

        }

        public Order(string userEmail,  DeliveryMethod deliveryMethod, ICollection<OrderItem> items,  string paymentIntentId)
        {
            buyerEmail = userEmail;
           
            DeliveryMethod = deliveryMethod;
            Items = items;
          
            PaymentIntentId = paymentIntentId;
        }

        public string buyerEmail { get; set; } = default!;

       

        public DeliveryMethod DeliveryMethod { get; set; } = default!;

        public ICollection<OrderItem> Items { get; set; } = [];

        public decimal SubTotal { get; set; }



        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public int DeliveryMethodId { get; set; }  //fk

        public OrderStatus Status { get; set; }

        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;
        public string PaymentIntentId { get; set; }
    }
}
