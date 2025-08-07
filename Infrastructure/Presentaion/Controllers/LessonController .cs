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
    public class LessonController(IServiceManager _serviceManager) :BaseController
    {
        [HttpGet("ByModule/{moduleId}")]
        public async Task<ActionResult> GetByModuleId(int moduleId)
        {
            var result = await _serviceManager.LessonServices.GetLessonsByModuleIdAsync(moduleId);
            return Ok(result);
        }

        [HttpGet("ByCourse/{courseId}")]
        public async Task<ActionResult> GetByCourseId(int courseId)
        {
            var result = await _serviceManager.LessonServices.GetLessonsByCourseIdAsync(courseId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _serviceManager.LessonServices.GetLessonByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] LessonDto lessonDto)
        {
            var result = await _serviceManager.LessonServices.CreateLessonAsync(lessonDto);
            return CreatedAtAction(nameof(GetById), new { id = result }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromForm] LessonDto lessonDto)
        {
            var result = await _serviceManager.LessonServices.UpdateLessonAsync(id, lessonDto);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _serviceManager.LessonServices.DeleteLessonAsync(id);
            return success ? NoContent() : NotFound();
        }

        [HttpGet("Search")]
        public async Task<ActionResult> Search([FromQuery] string keyword)
        {
            var result = await _serviceManager.LessonServices.SearchLessonsAsync(keyword);
            return Ok(result);
        }

        [HttpGet("TotalDuration/{moduleId}")]
        public async Task<ActionResult> GetTotalDuration(int moduleId)
        {
            var result = await _serviceManager.LessonServices.GetTotalDurationByModuleIdAsync(moduleId);
            return Ok(result);
        }
    }
}
