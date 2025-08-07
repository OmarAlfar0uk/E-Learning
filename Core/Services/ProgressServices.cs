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
    public class ProgressServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IProgressServices 
    {
        public async Task<IEnumerable<ProgressDto>> GetProgressByUserIdAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<Progress, int>();
            var progresses = await repo.FindByConditionAsync(p => p.AppUserId == userId);
            return _mapper.Map<IEnumerable<ProgressDto>>(progresses);
        }

        public async Task<IEnumerable<ProgressDto>> GetProgressByCourseIdAsync(int courseId)
        {
            var repo = _unitOfWork.GetRepository<Progress, int>();
            var progresses = await repo.FindByConditionAsync(p => p.CourseId == courseId);
            return _mapper.Map<IEnumerable<ProgressDto>>(progresses);
        }

        public async Task<IEnumerable<ProgressDto>> GetProgressByLessonIdAsync(int lessonId)
        {
            var repo = _unitOfWork.GetRepository<Progress, int>();
            var progresses = await repo.FindByConditionAsync(p => p.LessonId == lessonId);
            return _mapper.Map<IEnumerable<ProgressDto>>(progresses);
        }

        public async Task<ProgressDto?> GetProgressByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Progress, int>();
            var progress = await repo.GetByIdAsync(id);
            return progress is null ? null : _mapper.Map<ProgressDto>(progress);
        }

        public async Task<ProgressDto> AddProgressAsync(ProgressDto dto)
        {
            var repo = _unitOfWork.GetRepository<Progress, int>();
            var progress = _mapper.Map<Progress>(dto);
            await repo.AddAysnc(progress);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProgressDto>(progress);
        }

        public async Task<bool> MarkLessonAsCompletedAsync(int lessonId, string userId)
        {
            var repo = _unitOfWork.GetRepository<Progress, int>();
            var progresses = await repo.FindByConditionAsync(p => p.LessonId == lessonId && p.AppUserId == userId);
            var progress = progresses.FirstOrDefault();

            if (progress == null)
                return false;

            progress.IsCompleted = true;
            progress.UpdatedAt = DateTime.UtcNow;
            repo.Update(progress);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<double> GetCourseCompletionPercentageAsync(int courseId, string userId)
        {
            var repo = _unitOfWork.GetRepository<Progress, int>();
            var progresses = await repo.FindByConditionAsync(p => p.CourseId == courseId && p.AppUserId == userId);
            var total = progresses.Count();
            if (total == 0) return 0;

            var completed = progresses.Count(p => p.IsCompleted);
            return (completed * 100.0) / total;
        }
    }
}
