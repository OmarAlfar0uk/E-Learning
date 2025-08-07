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
    public class ReviewController(IServiceManager _serviceManager): BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _serviceManager.ReviewServices.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _serviceManager.ReviewServices.GetReviewByIdAsync(id);
            if (review == null) return NotFound();
            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ReviewDto dto)
        {
            var added = await _serviceManager.ReviewServices.AddReviewAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = added.Id }, added);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReviewDto dto)
        {
            var updated = await _serviceManager.ReviewServices.UpdateReviewAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _serviceManager.ReviewServices.DeleteReviewAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("course/{courseId}/user/{userId}/can-review")]
        public async Task<IActionResult> CanUserReview(int courseId, string userId)
        {
            var result = await _serviceManager.ReviewServices.CanUserReviewCourseAsync(courseId, userId);
            return Ok(result);
        }

        [HttpGet("course/{courseId}/user/{userId}/has-reviewed")]
        public async Task<IActionResult> HasUserReviewed(int courseId, string userId)
        {
            var result = await _serviceManager.ReviewServices.HasUserReviewedCourseAsync(courseId, userId);
            return Ok(result);
        }

        [HttpGet("course/{courseId}/average-rating")]
        public async Task<IActionResult> GetAverageRating(int courseId)
        {
            var result = await _serviceManager.ReviewServices.GetAverageRatingAsync(courseId);
            return Ok(result);
        }

        [HttpGet("course/{courseId}/total-reviews")]
        public async Task<IActionResult> GetTotalReviews(int courseId)
        {
            var result = await _serviceManager.ReviewServices.GetTotalReviewsAsync(courseId);
            return Ok(result);
        }

        [HttpGet("course/{courseId}/rating-distribution")]
        public async Task<IActionResult> GetRatingDistribution(int courseId)
        {
            var result = await _serviceManager.ReviewServices.GetRatingDistributionAsync(courseId);
            return Ok(result);
        }

        [HttpGet("course/{courseId}/with-users")]
        public async Task<IActionResult> GetReviewsWithUsers(int courseId)
        {
            var result = await _serviceManager.ReviewServices.GetCourseReviewsWithUsersAsync(courseId);
            return Ok(result);
        }

        [HttpPost("report")]
        public async Task<IActionResult> ReportReview(int reviewId, string reason)
        {
            var result = await _serviceManager.ReviewServices.ReportReviewAsync(reviewId, reason);
            if (!result) return NotFound();
            return Ok("Review reported successfully.");
        }
    }
}
