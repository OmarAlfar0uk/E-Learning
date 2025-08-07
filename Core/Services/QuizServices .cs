using AutoMapper;
using Domain.Contract;
using Domain.Models;
using ServicesAbstraction;
using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class QuizServices(IUnitOfWork _unitOfWork , IMapper _mapper) : IQuizServices
    {
        public async Task<QuizDto> CreateQuizAsync(QuizDto quizDto)
        {
            var quiz = _mapper.Map<Quiz>(quizDto);

            await _unitOfWork.GetRepository<Quiz, int>().AddAysnc(quiz);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<QuizDto>(quiz);
        }

        public async Task<bool> DeleteQuizAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Quiz, int>();
            var quiz = await repo.GetByIdAsync(id);
            if (quiz == null) return false;

            repo.Remove(quiz);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<QuizDto> GetQuizByIdAsync(int id)
        {
            var quiz = await _unitOfWork.GetRepository<Quiz, int>().GetByIdAsync(id);
            return _mapper.Map<QuizDto>(quiz);
        }

        public async Task<IEnumerable<QuizDto>> GetQuizzesByModuleIdAsync(int moduleId)
        {
            var quizzes = await _unitOfWork.GetRepository<Quiz, int>()
                .FindByConditionAsync(q => q.ModuleId == moduleId);
            return _mapper.Map<IEnumerable<QuizDto>>(quizzes);
        }

        public async Task<QuizDto> UpdateQuizAsync(int id, QuizDto quizDto)
        {
            var repo = _unitOfWork.GetRepository<Quiz, int>();
            var quiz = await repo.GetByIdAsync(id);
            if (quiz == null) return null!;

            quiz.Title = quizDto.Title;
            quiz.ModuleId = quizDto.ModuleId;
            quiz.CreatedById = quizDto.CreatedById; 
            repo.Update(quiz);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<QuizDto>(quiz);
        }


        public async Task<QuizEvaluationResultDto> EvaluateQuizAsync(int quizId, Dictionary<int, string> userAnswers)
        {
            var questionRepo = _unitOfWork.GetRepository<QuizQuestion, int>();
            var questions = await questionRepo.FindByConditionAsync(q => q.QuizId == quizId);

            int total = questions.Count();
            int correct = questions.Count(q =>
                userAnswers.TryGetValue(q.Id, out var answer) &&
                string.Equals(answer.Trim(), q.CorrectAnswer.Trim(), StringComparison.OrdinalIgnoreCase));

            return new QuizEvaluationResultDto
            {
                QuizId = quizId,
                TotalQuestions = total,
                CorrectAnswers = correct,
                ScorePercentage = total > 0 ? (correct * 100.0 / total) : 0
            };
        }

        public async Task<QuizStatisticsDto> GetQuizStatisticsAsync(int quizId)
        {
            var questionRepo = _unitOfWork.GetRepository<QuizQuestion, int>();
            var questions = await questionRepo.FindByConditionAsync(q => q.QuizId == quizId);

            return new QuizStatisticsDto
            {
                QuizId = quizId,
                TotalQuestions = questions.Count()
             
            };
        }

        public async Task<bool> CanUserAccessQuizAsync(int quizId, string userId)
        {
            var quiz = await _unitOfWork.GetRepository<Quiz, int>().GetByIdAsync(quizId);
            if (quiz == null) return false;

            return quiz.CreatedById == userId;
        }

    }
}
