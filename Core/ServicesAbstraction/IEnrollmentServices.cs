using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IEnrollmentServices
    {
        Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync();
        Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByUserIdAsync(string userId);
        Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByCourseIdAsync(int courseId);
        Task<EnrollmentDto?> GetEnrollmentByIdAsync(int id);
        Task<EnrollmentDto> EnrollUserInCourseAsync(EnrollmentDto dto);
        Task<bool> MarkAsCompletedAsync(int enrollmentId);
        Task<bool> DeleteEnrollmentAsync(int id);
        Task<double> GetUserProgressAsync(int courseId, string userId);
        Task<bool> HasUserEnrolledAsync(int courseId, string userId);
    }
}
