using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IReviewServices
    {
        Task<IEnumerable<ReviewDto>> GetAllReviewsAsync();
        Task<ReviewDto?> GetReviewByIdAsync(int id);
        Task<ReviewDto> AddReviewAsync(ReviewDto dto);
        Task<ReviewDto?> UpdateReviewAsync(int id, ReviewDto dto);
        Task<bool> DeleteReviewAsync(int id);

        
        Task<bool> HasUserReviewedCourseAsync(int courseId, string userId);

       
        Task<bool> CanUserReviewCourseAsync(int courseId, string userId);

        Task<double> GetAverageRatingAsync(int courseId);
        Task<int> GetTotalReviewsAsync(int courseId);
        Task<Dictionary<int, int>> GetRatingDistributionAsync(int courseId); 

        Task<IEnumerable<ReviewWithUserDto>> GetCourseReviewsWithUsersAsync(int courseId);
        Task<bool> ReportReviewAsync(int reviewId, string reason);
    }
}
