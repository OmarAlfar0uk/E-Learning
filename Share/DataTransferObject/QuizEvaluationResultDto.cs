using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.DataTransferObject
{
    public class QuizEvaluationResultDto
    {
        public int QuizId { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public double ScorePercentage { get; set; }
        public Dictionary<int, bool> QuestionResults { get; set; } = new();
    }
}
