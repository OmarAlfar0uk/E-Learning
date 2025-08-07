using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.DataTransferObject
{
    public class QuizDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int ModuleId { get; set; }
        public string CreatedById { get; set; }
        public List<QuizQuestionDto> Questions { get; set; } = new();
    }
}
