using Domain.Models.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Progress : BaseEntity<int>
    {
      

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public int? LessonId { get; set; }
        public Lesson? Lesson { get; set; }

        public string AppUserId { get; set; }
        public AppUsers AppUser { get; set; } = null!;

        public bool IsCompleted { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
    
}
