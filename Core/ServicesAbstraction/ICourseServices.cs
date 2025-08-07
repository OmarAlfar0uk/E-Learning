using Share;
using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface ICourseServices
    {
        Task<PaginatedResult<CourseDto>> GetAllCoursesAsync(CourseQueryParams queryParams);

        Task<CourseDto> GetCourseByIdAsync(int Id);

        Task<CourseDto> AddCourseAsync(CourseDto course);   

        Task<CourseDto> UpdateCourseAsync( int id ,CourseDto course);

        Task<bool> DeleteCourseAsync(int id);
        Task<IEnumerable<TypeDto>> GetAllTypeAsync();

        Task<IEnumerable<CourseDto>> GetTopRatedCoursesAsync(int count = 10);
        Task<IEnumerable<CourseDto>> GetCoursesByUserAsync(string userId);
        Task<IEnumerable<LessonDto>> GetLessonsByCourseIdAsync(int courseId);
        Task<bool> IsUserOwnerOfCourseAsync(int courseId, string userId);
        Task<CourseStatisticsDto> GetCourseStatisticsAsync(int courseId);
    }
}
