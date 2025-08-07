using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.DataTransferObject
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public DateTime EnrolledAt { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
        public double ProgressPercentage { get; set; }

        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;

        public string AppUserId { get; set; } = string.Empty;
        public string AppUserName { get; set; } = string.Empty;
    }
}
