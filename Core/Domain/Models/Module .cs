using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Module : BaseEntity<int> 
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
    }
}
