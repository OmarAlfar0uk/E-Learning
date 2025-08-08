using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentaion.Attribute;
using ServicesAbstraction;
using Share;
using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    public class CourseController(IServiceManager _serviceManager) : BaseController
    {
        [HttpGet]
        [Cache]
        public async Task<ActionResult<PaginatedResult<CourseDto>>> GetAllCourses([FromQuery]CourseQueryParams queryParams)
        {
            try
            {
                var courses = await _serviceManager.CourseServices.GetAllCoursesAsync(queryParams);
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            try
            {
                var course = await _serviceManager.CourseServices.GetCourseByIdAsync(id);
                if (course == null)
                {
                    return NotFound("Course not found or deleted.");
                }
                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            try
            {
                var types = await _serviceManager.CourseServices.GetAllTypeAsync();
                return Ok(types);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CourseDto>> AddCourse([FromForm] CourseDto courseDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized("You must be logged in to create a course.");
                }

                var createdCourse = await _serviceManager.CourseServices.AddCourseAsync(courseDto);
                return CreatedAtAction(nameof(GetCourse), new { id = createdCourse.Id }, createdCourse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult<CourseDto>> UpdateCourse(int id, [FromForm] CourseDto courseDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var isOwner = await _serviceManager.CourseServices.IsUserOwnerOfCourseAsync(id, User.Identity.Name);
                if (!isOwner)
                {
                    return Unauthorized("You are not authorized to update this course.");
                }

                var updatedCourse = await _serviceManager.CourseServices.UpdateCourseAsync(id, courseDto);
                if (updatedCourse == null)
                {
                    return NotFound("Course not found or deleted.");
                }

                return Ok(updatedCourse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteCourse(int id)
        {
            try
            {
                var isOwner = await _serviceManager.CourseServices.IsUserOwnerOfCourseAsync(id, User.Identity.Name);
                if (!isOwner)
                {
                    return Unauthorized("You are not authorized to delete this course.");
                }

                var deleted = await _serviceManager.CourseServices.DeleteCourseAsync(id);
                if (!deleted)
                {
                    return NotFound("Course not found or already deleted.");
                }

                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("top-rated")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetTopRatedCourses([FromQuery] int count = 10)
        {
            try
            {
                if (count <= 0)
                {
                    return BadRequest("Count must be greater than zero.");
                }

                var courses = await _serviceManager.CourseServices.GetTopRatedCoursesAsync(count);
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("instructor")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByInstructor()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized("You must be logged in to view your courses.");
                }

                var courses = await _serviceManager.CourseServices.GetCoursesByUserAsync(User.Identity.Name);
                return Ok(courses);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}/lessons")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessonsByCourseId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid course ID.");
                }

                var lessons = await _serviceManager.CourseServices.GetLessonsByCourseIdAsync(id);
                return Ok(lessons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}/is-owner")]
        [Authorize]
        public async Task<ActionResult<bool>> IsUserOwnerOfCourse(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid course ID.");
                }

                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized("You must be logged in to check course ownership.");
                }

                var isOwner = await _serviceManager.CourseServices.IsUserOwnerOfCourseAsync(id, User.Identity.Name);
                return Ok(isOwner);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}/statistics")]
        public async Task<ActionResult<CourseStatisticsDto>> GetCourseStatistics(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid course ID.");
                }

                var statistics = await _serviceManager.CourseServices.GetCourseStatisticsAsync(id);
                if (statistics == null)
                {
                    return NotFound("Course not found or deleted.");
                }
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
