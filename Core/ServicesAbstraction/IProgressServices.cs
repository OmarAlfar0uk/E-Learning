using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IProgressServices
    {
            Task<IEnumerable<ProgressDto>> GetProgressByUserIdAsync(string userId);
            Task<IEnumerable<ProgressDto>> GetProgressByCourseIdAsync(int courseId);
            Task<IEnumerable<ProgressDto>> GetProgressByLessonIdAsync(int lessonId);
            Task<ProgressDto?> GetProgressByIdAsync(int id);

            Task<ProgressDto> AddProgressAsync(ProgressDto progressDto);
            Task<bool> MarkLessonAsCompletedAsync(int lessonId, string userId);
            Task<double> GetCourseCompletionPercentageAsync(int courseId, string userId);
    }
}
