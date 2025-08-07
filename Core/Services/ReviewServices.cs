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
    public class ReviewServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IReviewServices
    {
        public async Task<ReviewDto> AddReviewAsync(ReviewDto dto)
        {
            var repository = _unitOfWork.GetRepository<Review, int>();
            var review = _mapper.Map<Review>(dto);
            review.CreatedAt = DateTime.UtcNow;
            await repository.AddAysnc(review);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<Review, int>();
            var review = await repository.GetByIdAsync(id);
            if (review == null) return false;

            repository.Remove(review);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
        {
            var repository = _unitOfWork.GetRepository<Review, int>();
            var reviews = await repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task<ReviewDto?> GetReviewByIdAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<Review, int>();
            var review = await repository.GetByIdAsync(id);
            return _mapper.Map<ReviewDto?>(review);
        }

        public async Task<ReviewDto?> UpdateReviewAsync(int id, ReviewDto dto)
        {
            var repository = _unitOfWork.GetRepository<Review, int>();
            var review = await repository.GetByIdAsync(id);
            if (review == null) return null;

            review.Rating = dto.Rating;
            review.Comment = dto.Comment;
            review.CreatedAt = DateTime.UtcNow;

            repository.Update(review);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<bool> HasUserReviewedCourseAsync(int courseId, string userId)
        {
            var repository = _unitOfWork.GetRepository<Review, int>();
            var result = await repository.FindByConditionAsync(r => r.CourseId == courseId && r.AppUserId == userId);
            return result.Any();
        }

        public async Task<bool> CanUserReviewCourseAsync(int courseId, string userId)
        {
        
            return !await HasUserReviewedCourseAsync(courseId, userId);
        }

        public async Task<double> GetAverageRatingAsync(int courseId)
        {
            var repository = _unitOfWork.GetRepository<Review, int>();
            var reviews = await repository.FindByConditionAsync(r => r.CourseId == courseId);
            if (!reviews.Any()) return 0;
            return reviews.Average(r => r.Rating);
        }

        public async Task<int> GetTotalReviewsAsync(int courseId)
        {
            var repository = _unitOfWork.GetRepository<Review, int>();
            var reviews = await repository.FindByConditionAsync(r => r.CourseId == courseId);
            return reviews.Count();
        }

        public async Task<Dictionary<int, int>> GetRatingDistributionAsync(int courseId)
        {
            var repository = _unitOfWork.GetRepository<Review, int>();
            var reviews = await repository.FindByConditionAsync(r => r.CourseId == courseId);

            return reviews
                .GroupBy(r => r.Rating)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public async Task<IEnumerable<ReviewWithUserDto>> GetCourseReviewsWithUsersAsync(int courseId)
        {
            var repository = _unitOfWork.GetRepository<Review, int>();
            var reviews = await repository.FindByConditionAsync(r => r.CourseId == courseId);
            return _mapper.Map<IEnumerable<ReviewWithUserDto>>(reviews);
        }

        public async Task<bool> ReportReviewAsync(int reviewId, string reason)
        {
            var repository = _unitOfWork.GetRepository<Review, int>();
            var review = await repository.GetByIdAsync(reviewId);
            if (review == null) return false;

            review.IsReported = true;
            review.ReportReason = reason;

            repository.Update(review);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
