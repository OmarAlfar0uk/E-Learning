using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class QuizQuestion : BaseEntity<int>
    {
        public string QuestionText { get; set; } = null!;
        public string OptionA { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;
        public string CorrectAnswer { get; set; } = null!; 

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } = null!;
    }
  
}
