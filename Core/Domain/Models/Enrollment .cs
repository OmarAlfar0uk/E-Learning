using Domain.Models.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Enrollment : BaseEntity<int>
    {

        public DateTime EnrolledAt { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public string AppUserId { get; set; }
        public AppUsers AppUser { get; set; } = null!;

        public double ProgressPercentage { get; set; } = 0;
      
    }
}
