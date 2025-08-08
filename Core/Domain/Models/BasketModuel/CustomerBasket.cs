﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BasketModuel
{
    public class CustomerBasket
    {

        public string Id { get; set; } // GUID : creating By Front End

        public ICollection<BasketItem> Items { get; set; } = [];

        public string? clintSecret { get; set; }

        public string? PaymentIntentId { get; set; }

        public int? deliveryMethodId { get; set; }

        public decimal? shippingPrice { get; set; }
    }
}
