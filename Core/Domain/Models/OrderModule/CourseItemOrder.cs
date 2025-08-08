using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModule
{
    public class CourseItemOrder
    {
        public int CoursetId { get; set; }

        public string CourseName { get; set; } = default!;

        public string PictureUrl { get; set; } = default!;
    }
}
