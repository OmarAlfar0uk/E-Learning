using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.DataTransferObject
{
    public class QuizStatisticsDto
    {
        public int QuizId { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalAttempts { get; set; }
        public double AverageScore { get; set; }
        public int MaxScore { get; set; }
        public int MinScore { get; set; }
    }
}
