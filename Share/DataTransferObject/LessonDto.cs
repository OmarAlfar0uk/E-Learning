using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.DataTransferObject
{
    public class LessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public IFormFile VideoFile { get; set; } = null!;
        public string? VideoUrl { get; set; }
        public TimeSpan Duration { get; set; }

        public int CourseId { get; set; }
        public int ModuleId { get; set; }
    }

}
