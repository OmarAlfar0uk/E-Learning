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
    internal class LessonServices(IUnitOfWork _unitOfWork , IMapper _mapper , ICloudinaryService _cloudinaryService) : ILessonServices
    {
        public async Task<LessonDto> CreateLessonAsync(LessonDto dto)
        {
            var videoUrl = await _cloudinaryService.UploadVideoAsync(dto.VideoFile);

            var lesson = new Lesson
            {
                Title = dto.Title,
                VideoUrl = videoUrl,
                Duration = dto.Duration,
                CourseId = dto.CourseId,
                ModuleId = dto.ModuleId
            };

            await _unitOfWork.GetRepository<Lesson, int>().AddAysnc(lesson);
            await _unitOfWork.SaveChangesAsync();

            dto.VideoUrl = videoUrl;
            return dto;
        }

        public async Task<bool> DeleteLessonAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Lesson, int>();
            var lesson = await repo.GetByIdAsync(id);
            if (lesson is null) return false;

            repo.Remove(lesson);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<LessonDto> GetLessonByIdAsync(int id)
        {
            var lesson = await _unitOfWork.GetRepository<Lesson, int>().GetByIdAsync(id);
            return _mapper.Map<LessonDto>(lesson);
        }

        public async Task<IEnumerable<LessonDto>> GetLessonsByCourseIdAsync(int courseId)
        {
            var lessons = await _unitOfWork.GetRepository<Lesson, int>().GetAllAsync();
            return lessons.Where(l => l.CourseId == courseId)
                          .Select(l => _mapper.Map<LessonDto>(l));
        }

        public async Task<IEnumerable<LessonDto>> GetLessonsByModuleIdAsync(int moduleId)
        {
            var lessons = await _unitOfWork.GetRepository<Lesson, int>().GetAllAsync();
            return lessons.Where(l => l.ModuleId == moduleId)
                          .Select(l => _mapper.Map<LessonDto>(l));
        }

        public async Task<LessonDto> UpdateLessonAsync(int id, LessonDto dto)
        {
            var repo = _unitOfWork.GetRepository<Lesson, int>();
            var lesson = await repo.GetByIdAsync(id);
            if (lesson is null) return null!;

            if (dto.VideoFile != null)
            {
                var newUrl = await _cloudinaryService.UploadVideoAsync(dto.VideoFile);
                lesson.VideoUrl = newUrl;
            }

            lesson.Title = dto.Title;
            lesson.Duration = dto.Duration;
            lesson.CourseId = dto.CourseId;
            lesson.ModuleId = dto.ModuleId;

            repo.Update(lesson);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<LessonDto>(lesson);
        }

        public async Task<IEnumerable<LessonDto>> SearchLessonsAsync(string keyword)
        {
            var lessons = await _unitOfWork.GetRepository<Lesson, int>().GetAllAsync();
            return lessons.Where(l => l.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                          .Select(l => _mapper.Map<LessonDto>(l));
        }

        public async Task<TimeSpan> GetTotalDurationByModuleIdAsync(int moduleId)
        {
            var lessons = await _unitOfWork.GetRepository<Lesson, int>().GetAllAsync();
            return lessons.Where(l => l.ModuleId == moduleId)
                          .Select(l => l.Duration)
                          .Aggregate(TimeSpan.Zero, (total, next) => total + next);
        }
    }
}
