    using Share.DataTransferObject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace ServicesAbstraction
    {
        public interface ILessonServices
        {
            Task<IEnumerable<LessonDto>> GetLessonsByModuleIdAsync(int moduleId);
            Task<LessonDto> GetLessonByIdAsync(int id);
            Task<LessonDto> CreateLessonAsync(LessonDto lessonDto);
            Task<LessonDto> UpdateLessonAsync(int id, LessonDto lessonDto);
            Task<bool> DeleteLessonAsync(int id);
            Task<IEnumerable<LessonDto>> GetLessonsByCourseIdAsync(int courseId);
            Task<IEnumerable<LessonDto>> SearchLessonsAsync(string keyword);
            Task<TimeSpan> GetTotalDurationByModuleIdAsync(int moduleId);

        }
    }
