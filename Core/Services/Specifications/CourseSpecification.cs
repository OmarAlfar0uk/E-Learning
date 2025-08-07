using Domain.Models;
using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class CourseSpecification :BaseSpecifications<Course , int>
    {
        public CourseSpecification(CourseQueryParams queryParams) : base(C => (string.IsNullOrWhiteSpace(queryParams.SearchValue) || C.Name.Contains(queryParams.SearchValue)))
        {
            switch(queryParams.sortingOptions)
            {
                case CourseSortingOptions.NameAsc:
                    AddOrderBy(C=>C.Name);
                    break;

                case CourseSortingOptions.NameDesc:
                    AddOrderByDescending(C=>C.Name);
                    break;

                case CourseSortingOptions.PriceAsc:
                    AddOrderByDescending(C=>C.Price);
                    break;

                case CourseSortingOptions.PriceDesc:
                    AddOrderByDescending(C=>C.Price);
                    break;

                    default:
                    break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageNumber);
        }
    }
}
