    using AutoMapper;
    using CloudinaryDotNet;
    using Domain.Contract;
    using Domain.Models;
using Services.Specifications;
using ServicesAbstraction;
using Share;
using Share.DataTransferObject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

namespace Services
{
    public class CourseServices(IUnitOfWork _unitOfWork, IMapper _mapper, Cloudinary _cloudinary, ICloudinaryService _cloudinaryService) : ICourseServices
    {
        public async Task<PaginatedResult<CourseDto>> GetAllCoursesAsync(CourseQueryParams queryParams)
        {
            var Repo = _unitOfWork.GetRepository<Course, int>();
            var Specification = new CourseSpecification(queryParams);
            var Course = await Repo.GetAllAsync(Specification);
            var Data = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(Course);
            var ProductCount = Course.Count();
            var CountSpec = new CourseCountSpecification(queryParams);
            var TotalCount = await Repo.CountAsync(CountSpec);
             return new PaginatedResult<CourseDto>(queryParams.PageNumber, ProductCount, TotalCount, Data);
             
        }

        public async Task<CourseDto> GetCourseByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var course = await _unitOfWork.GetRepository<Course, int>().GetByIdAsync(id);
            if (course == null || course.IsDeleted)
            {
                return null;
            }

            return _mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> AddCourseAsync(CourseDto courseDto)
        {
            if (courseDto == null)
            {
                throw new ArgumentNullException(nameof(courseDto));
            }

            var course = _mapper.Map<Course>(courseDto);
            // Remove CreatedAt if not in Course model
            // course.CreatedAt = DateTime.UtcNow;
            course.IsDeleted = false;

            // Handle picture upload
            if (courseDto.PictureFile != null)
            {
                course.PictureUrl = await _cloudinaryService.UploadImageAsync(courseDto.PictureFile);
            }

            // Handle promo video upload
            if (courseDto.PromoVideoFile != null)
            {
                course.PromoVideoUrl = await _cloudinaryService.UploadVideoAsync(courseDto.PromoVideoFile);
            }

            await _unitOfWork.GetRepository<Course, int>().AddAysnc(course); // Fixed typo: AddAysnc -> AddAsync
            await _unitOfWork.SaveChangesAsync(); // Replaced SaveChangesAsync with CompleteAsync

            return _mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> UpdateCourseAsync(int id, CourseDto courseDto)
        {
            if (id <= 0)
            {
                return null;
            }

            if (courseDto == null)
            {
                throw new ArgumentNullException(nameof(courseDto));
            }

            var course = await _unitOfWork.GetRepository<Course, int>().GetByIdAsync(id);
            if (course == null || course.IsDeleted)
            {
                return null;
            }

            // Store old URLs for deletion
            var oldPictureUrl = course.PictureUrl;
            var oldPromoVideoUrl = course.PromoVideoUrl;

            _mapper.Map(courseDto, course);
            // Remove UpdatedAt if not in Course model
            // course.UpdatedAt = DateTime.UtcNow;

            // Handle picture update
            if (courseDto.PictureFile != null)
            {
                course.PictureUrl = await _cloudinaryService.UploadImageAsync(courseDto.PictureFile);
                // Delete old picture if it exists
                if (!string.IsNullOrEmpty(oldPictureUrl))
                {
                    var publicId = ExtractPublicId(oldPictureUrl);
                    if (!string.IsNullOrEmpty(publicId))
                    {
                        await _cloudinaryService.DeleteFileAsync(publicId);
                    }
                }
            }

            // Handle promo video update
            if (courseDto.PromoVideoFile != null)
            {
                course.PromoVideoUrl = await _cloudinaryService.UploadVideoAsync(courseDto.PromoVideoFile);
                // Delete old video if it exists
                if (!string.IsNullOrEmpty(oldPromoVideoUrl))
                {
                    var publicId = ExtractPublicId(oldPromoVideoUrl);
                    if (!string.IsNullOrEmpty(publicId))
                    {
                        await _cloudinaryService.DeleteFileAsync(publicId);
                    }
                }
            }

            _unitOfWork.GetRepository<Course, int>().Update(course);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CourseDto>(course);
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            var course = await _unitOfWork.GetRepository<Course, int>().GetByIdAsync(id);
            if (course == null || course.IsDeleted)
            {
                return false;
            }

            // Delete associated media from Cloudinary
            if (!string.IsNullOrEmpty(course.PictureUrl))
            {
                var publicId = ExtractPublicId(course.PictureUrl);
                if (!string.IsNullOrEmpty(publicId))
                {
                    await _cloudinaryService.DeleteFileAsync(publicId);
                }
            }

            if (!string.IsNullOrEmpty(course.PromoVideoUrl))
            {
                var publicId = ExtractPublicId(course.PromoVideoUrl);
                if (!string.IsNullOrEmpty(publicId))
                {
                    await _cloudinaryService.DeleteFileAsync(publicId);
                }
            }

            course.IsDeleted = true;
   

            _unitOfWork.GetRepository<Course, int>().Update(course);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypeAsync()
        {
            var types = await _unitOfWork.GetRepository<CourseType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDto>>(types);
        }

      
        public async Task<IEnumerable<CourseDto>> GetTopRatedCoursesAsync(int count = 10)
        {
            if (count <= 0)
            {
                return Enumerable.Empty<CourseDto>();
            }

            var courses = await _unitOfWork.GetRepository<Course, int>().FindByConditionAsync(c => !c.IsDeleted);
            var result = courses
                .OrderByDescending(c => c.Reviews != null && c.Reviews.Any() ? c.Reviews.Average(r => r.Rating) : 0)
                .Take(count)
                .ToList();

            return _mapper.Map<IEnumerable<CourseDto>>(result);
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var courses = await _unitOfWork.GetRepository<Course, int>().FindByConditionAsync(c =>
                c.InstructorId == userId && !c.IsDeleted);

            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<IEnumerable<LessonDto>> GetLessonsByCourseIdAsync(int courseId)
        {
            if (courseId <= 0)
            {
                return Enumerable.Empty<LessonDto>();
            }

            var course = await _unitOfWork.GetRepository<Course, int>().GetByIdAsync(courseId);
            if (course == null || course.IsDeleted)
            {
                return Enumerable.Empty<LessonDto>();
            }

            return _mapper.Map<IEnumerable<LessonDto>>(course.Lessons ?? Enumerable.Empty<Lesson>());
        }

        public async Task<bool> IsUserOwnerOfCourseAsync(int courseId, string userId)
        {
            if (courseId <= 0 || string.IsNullOrWhiteSpace(userId))
            {
                return false;
            }

            var course = await _unitOfWork.GetRepository<Course, int>().GetByIdAsync(courseId);
            return course != null && !course.IsDeleted && course.InstructorId == userId;
        }

        public async Task<CourseStatisticsDto> GetCourseStatisticsAsync(int courseId)
        {
            if (courseId <= 0)
            {
                return null;
            }

            var course = await _unitOfWork.GetRepository<Course, int>().GetByIdAsync(courseId);
            if (course == null || course.IsDeleted)
            {
                return null;
            }

            var enrollments = await _unitOfWork.GetRepository<Enrollment, int>().FindByConditionAsync(e => e.CourseId == courseId);
            var reviews = await _unitOfWork.GetRepository<Review, int>().FindByConditionAsync(r => r.CourseId == courseId);
            var lessons = await _unitOfWork.GetRepository<Lesson, int>().FindByConditionAsync(l => l.CourseId == courseId);

            var totalEnrollments = enrollments.Count();
            var completedEnrollments = enrollments.Count(e => e.IsCompleted);
            var completionRate = totalEnrollments > 0 ? (double)completedEnrollments / totalEnrollments * 100 : 0;

            var ratingDistribution = reviews
                .GroupBy(r => r.Rating)
                .ToDictionary(g => g.Key, g => g.Count());

            return new CourseStatisticsDto
            {
                CourseId = courseId,
                CourseTitle = course.Title,
                TotalEnrollments = totalEnrollments,
                TotalReviews = reviews.Count(),
                AverageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0,
                TotalLessons = lessons.Count(),
                CompletionRate = completionRate,
                RatingDistribution = ratingDistribution
            };
        }

        private string ExtractPublicId(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            // Assuming URL format: "courses/images/publicId.jpg" or "courses/videos/publicId.mp4"
            var parts = url.Split('/');
            if (parts.Length < 3)
            {
                return null;
            }

            var fileName = parts[^1]; // Last part (e.g., "publicId.jpg")
            return fileName.Substring(0, fileName.LastIndexOf('.')); // Remove extension
        }

        
    }
}