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
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();

        Task<CourseDto> GetCourseByIdAsync(int Id);

        Task<CourseDto> AddCourseAsync(CourseDto course);   

        Task<CourseDto> UpdateCourseAsync( int id ,CourseDto course);

        Task<bool> DeleteCourseAsync(int id);
        Task<IEnumerable<TypeDto>> GetAllTypeAsync();
    }
}
