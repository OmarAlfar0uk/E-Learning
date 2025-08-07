using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.DataTransferObject
{
    public class ProgressDto
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public int? LessonId { get; set; }

        public string AppUserId { get; set; } = null!;

        public bool IsCompleted { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
