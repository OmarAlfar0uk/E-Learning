using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Lesson : BaseEntity<int>
    {
        public string Title { get; set; } = null!;
        public string VideoUrl { get; set; } = null!;
        public TimeSpan Duration { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public int ModuleId { get; set; }
        public Module Module { get; set; } = null!;
    }
}
