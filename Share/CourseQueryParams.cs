using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share
{
    public class CourseQueryParams
    {
        private const int DafaultPageSize = 5;
        private const int MaxPageSize = 10;
        public  CourseSortingOptions sortingOptions { get; set; }
        public string?  SearchValue { get; set; }
        public int PageNumber { get; set; } = 1;

        private int pageSize = DafaultPageSize;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
    }
}
