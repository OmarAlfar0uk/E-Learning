using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModule
{
    public class OrderItem : BaseEntity<int>
    {
        public CourseItemOrder Course { get; set; } = default!;

        public decimal Price { get; set; }

    
    }
}
