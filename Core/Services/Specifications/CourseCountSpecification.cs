using Domain.Models;
using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class CourseCountSpecification : BaseSpecifications<Course, int>
    {
        public CourseCountSpecification(CourseQueryParams queryParams) : base(P => (P.Name != null && P.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            
        }
    }
}
