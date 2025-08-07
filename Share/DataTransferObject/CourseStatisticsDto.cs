using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.DataTransferObject
{
    public class CourseStatisticsDto
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;

        public int TotalEnrollments { get; set; } 
        public int TotalReviews { get; set; }  
        public double AverageRating { get; set; }
        public int TotalLessons { get; set; }     
        public double CompletionRate { get; set; } 

        public Dictionary<int, int>? RatingDistribution { get; set; } 
    }
}
