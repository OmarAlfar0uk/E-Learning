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
    public class ProgressController(IServiceManager _serviceManager):BaseController
    {
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var result = await _serviceManager.ProgressServices.GetProgressByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("course/{courseId:int}")]
        public async Task<IActionResult> GetByCourseId(int courseId)
        {
            var result = await _serviceManager.ProgressServices.GetProgressByCourseIdAsync(courseId);
            return Ok(result);
        }

        [HttpGet("lesson/{lessonId:int}")]
        public async Task<IActionResult> GetByLessonId(int lessonId)
        {
            var result = await _serviceManager.ProgressServices.GetProgressByLessonIdAsync(lessonId);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _serviceManager.ProgressServices.GetProgressByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProgress([FromBody] ProgressDto dto)
        {
            var result = await _serviceManager.ProgressServices.AddProgressAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("complete")]
        public async Task<IActionResult> MarkLessonAsCompleted([FromQuery] int lessonId, [FromQuery] string userId)
        {
            var success = await _serviceManager.ProgressServices.MarkLessonAsCompletedAsync(lessonId, userId);
            if (!success) return NotFound("Progress not found.");
            return NoContent();
        }

        [HttpGet("completion-percentage")]
        public async Task<IActionResult> GetCourseCompletionPercentage([FromQuery] int courseId, [FromQuery] string userId)
        {
            var percentage = await _serviceManager.ProgressServices.GetCourseCompletionPercentageAsync(courseId, userId);
            return Ok(percentage);
        }
    }
}
