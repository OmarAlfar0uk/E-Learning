using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    public class QuizController(IServiceManager _serviceManager) :BaseController
    {
        [HttpPost]
        public async Task<ActionResult> CreateQuiz([FromBody] QuizDto quizDto)
        {
            var result = await _serviceManager.QuizServices.CreateQuizAsync(quizDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetQuizById(int id)
        {
            var result = await _serviceManager.QuizServices.GetQuizByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("module/{moduleId}")]
        public async Task<ActionResult> GetQuizzesByModule(int moduleId)
        {
            var result = await _serviceManager.QuizServices.GetQuizzesByModuleIdAsync(moduleId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateQuiz(int id, [FromBody] QuizDto quizDto)
        {
            var result = await _serviceManager.QuizServices.UpdateQuizAsync(id, quizDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuiz(int id)
        {
            var success = await _serviceManager.QuizServices.DeleteQuizAsync(id);
            return success ? NoContent() : NotFound();
        }

        [HttpPost("{quizId}/evaluate")]
        public async Task<ActionResult> EvaluateQuiz(int quizId, [FromBody] Dictionary<int, string> userAnswers)
        {
            var result = await _serviceManager.QuizServices.EvaluateQuizAsync(quizId, userAnswers);
            return Ok(result);
        }

        [HttpGet("{quizId}/statistics")]
        public async Task<ActionResult> GetQuizStatistics(int quizId)
        {
            var stats = await _serviceManager.QuizServices.GetQuizStatisticsAsync(quizId);
            return Ok(stats);
        }

        [HttpGet("{quizId}/can-access/{userId}")]
        public async Task<ActionResult> CanUserAccessQuiz(int quizId, string userId)
        {
            var canAccess = await _serviceManager.QuizServices.CanUserAccessQuizAsync(quizId, userId);
            return Ok(new { CanAccess = canAccess });
        }
    }
}
