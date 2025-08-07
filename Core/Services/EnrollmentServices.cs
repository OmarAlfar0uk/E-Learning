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
    public class EnrollmentServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IEnrollmentServices
    {

        public async Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync()
        {
            var repo = _unitOfWork.GetRepository<Enrollment, int>();
            var enrollments = await repo.GetAllAsync();
            return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
        }

        public async Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByUserIdAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<Enrollment, int>();
            var result = await repo.FindByConditionAsync(e => e.AppUserId == userId);
            return _mapper.Map<IEnumerable<EnrollmentDto>>(result);
        }

        public async Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByCourseIdAsync(int courseId)
        {
            var repo = _unitOfWork.GetRepository<Enrollment, int>();
            var result = await repo.FindByConditionAsync(e => e.CourseId == courseId);
            return _mapper.Map<IEnumerable<EnrollmentDto>>(result);
        }

        public async Task<EnrollmentDto?> GetEnrollmentByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Enrollment, int>();
            var result = await repo.GetByIdAsync(id);
            return result is null ? null : _mapper.Map<EnrollmentDto>(result);
        }

        public async Task<EnrollmentDto> EnrollUserInCourseAsync(EnrollmentDto dto)
        {
            var repo = _unitOfWork.GetRepository<Enrollment, int>();
            var entity = _mapper.Map<Enrollment>(dto);

            entity.EnrolledAt = DateTime.UtcNow;
            await repo.AddAysnc(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<EnrollmentDto>(entity);
        }

        public async Task<bool> MarkAsCompletedAsync(int enrollmentId)
        {
            var repo = _unitOfWork.GetRepository<Enrollment, int>();
            var entity = await repo.GetByIdAsync(enrollmentId);

            if (entity == null)
                return false;

            entity.IsCompleted = true;
            repo.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteEnrollmentAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Enrollment, int>();
            var entity = await repo.GetByIdAsync(id);

            if (entity == null)
                return false;

            repo.Remove(entity);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<double> GetUserProgressAsync(int courseId, string userId)
        {
            var repo = _unitOfWork.GetRepository<Enrollment, int>();
            var enrollments = await repo.FindByConditionAsync(e => e.CourseId == courseId && e.AppUserId == userId);

            if (!enrollments.Any()) return 0;

            var completed = enrollments.Count(e => e.IsCompleted);
            var total = enrollments.Count();

            return Math.Round((double)completed / total * 100, 2);
        }

        public async Task<bool> HasUserEnrolledAsync(int courseId, string userId)
        {
            var repo = _unitOfWork.GetRepository<Enrollment, int>();
            var result = await repo.FindByConditionAsync(e => e.CourseId == courseId && e.AppUserId == userId);
            return result.Any();
        }
    }
}
