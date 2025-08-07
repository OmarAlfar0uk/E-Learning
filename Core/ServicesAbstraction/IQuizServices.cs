using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IQuizServices
    {
        Task<QuizDto> CreateQuizAsync(QuizDto quizDto);
        Task<QuizDto> GetQuizByIdAsync(int id);
        Task<IEnumerable<QuizDto>> GetQuizzesByModuleIdAsync(int moduleId);
        Task<QuizDto> UpdateQuizAsync(int id, QuizDto quizDto);
        Task<bool> DeleteQuizAsync(int id);

        Task<QuizEvaluationResultDto> EvaluateQuizAsync(int quizId, Dictionary<int, string> userAnswers);
        Task<QuizStatisticsDto> GetQuizStatisticsAsync(int quizId);
        Task<bool> CanUserAccessQuizAsync(int quizId, string userId);
    }
}
