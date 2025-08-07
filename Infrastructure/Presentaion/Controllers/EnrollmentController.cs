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
    public class EnrollmentController(IServiceManager _serviceManager) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _serviceManager.EnrollmentServices.GetAllEnrollmentsAsync();
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var result = await _serviceManager.EnrollmentServices.GetEnrollmentsByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("course/{courseId:int}")]
        public async Task<IActionResult> GetByCourseId(int courseId)
        {
            var result = await _serviceManager.EnrollmentServices.GetEnrollmentsByCourseIdAsync(courseId);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _serviceManager.EnrollmentServices.GetEnrollmentByIdAsync(id);
            if (result is null)
                return NotFound($"Enrollment with ID {id} not found.");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Enroll([FromBody] EnrollmentDto dto)
        {
            var result = await _serviceManager.EnrollmentServices.EnrollUserInCourseAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("complete/{id:int}")]
        public async Task<IActionResult> MarkAsCompleted(int id)
        {
            var success = await _serviceManager.EnrollmentServices.MarkAsCompletedAsync(id);
            if (!success)
                return NotFound($"Enrollment with ID {id} not found.");
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _serviceManager.EnrollmentServices.DeleteEnrollmentAsync(id);
            if (!success)
                return NotFound($"Enrollment with ID {id} not found.");
            return NoContent();
        }

        [HttpGet("progress/{courseId:int}/user/{userId}")]
        public async Task<IActionResult> GetProgress(int courseId, string userId)
        {
            var progress = await _serviceManager.EnrollmentServices.GetUserProgressAsync(courseId, userId);
            return Ok(new { ProgressPercentage = progress });
        }

        [HttpGet("has-enrolled/{courseId:int}/user/{userId}")]
        public async Task<IActionResult> HasUserEnrolled(int courseId, string userId)
        {
            var enrolled = await _serviceManager.EnrollmentServices.HasUserEnrolledAsync(courseId, userId);
            return Ok(new { IsEnrolled = enrolled });
        }
    }
}
