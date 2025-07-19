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
    public class CourseServices(IUnitOfWork _unitOfWork, IMapper _mapper) : ICourseServices
    {
        public async Task<CourseDto> AddCourseAsync(CourseDto courseDto)
        {

            var course = _mapper.Map<Course>(courseDto);
            var courseTypeRepository = _unitOfWork.GetRepository<CourseType, int>();
            var courseTypes = await courseTypeRepository.GetAllAsync();
            var courseType = courseTypes.FirstOrDefault(ct => ct.Name == courseDto.TypeName);

            if (courseType == null)
            {
                throw new Exception("CourseType not found");
            }
            course.TypeId = courseType.Id;
            var categoryRepository = _unitOfWork.GetRepository<Category, int>();
            var category = await categoryRepository.GetByIdAsync(courseDto.CategoryId);

            if (category == null)
            {
                throw new Exception("Category not found");
            }
            course.CategoryId = category.Id;
            var courseRepository = _unitOfWork.GetRepository<Course, int>();
            await courseRepository.AddAysnc(course);

            await _unitOfWork.SaveChangesAsync();   
            var resultDto = _mapper.Map<CourseDto>(course);

            return resultDto;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var courseRepository = _unitOfWork.GetRepository<Course, int>();
            var course = await courseRepository.GetByIdAsync(id);

            if (course == null)
                 return false;
            
            courseRepository.Remove(course);
            await _unitOfWork.SaveChangesAsync();

            return true; 
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var Repo = _unitOfWork.GetRepository<Course, int>();
            var Courses = await Repo.GetAllAsync();
            var CourseDTO = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(Courses);
            return CourseDTO;
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypeAsync()
        {
            var Repo = _unitOfWork.GetRepository<CourseType, int>();
            var Type = await Repo.GetAllAsync();
            var TypeDTO = _mapper.Map<IEnumerable<CourseType>, IEnumerable<TypeDto>>(Type);
            return TypeDTO;
        }

        public async Task<CourseDto> GetCourseByIdAsync(int Id)
        {
            var Course =await  _unitOfWork.GetRepository<Course , int >().GetByIdAsync(Id);
            return _mapper.Map<Course , CourseDto>(Course);
        }

        public async Task<CourseDto> UpdateCourseAsync(int id, CourseDto courseDto)
        {
           
            if (id != courseDto.Id)
            {
                throw new ArgumentException("Course ID in DTO does not match the provided ID");
            }
            if (string.IsNullOrEmpty(courseDto.Title))
            {
                throw new ArgumentException("Course title is required");
            }
            if (courseDto.Price < 0)
            {
                throw new ArgumentException("Price cannot be negative");
            }
            var courseRepository = _unitOfWork.GetRepository<Course, int>();
            var course = await courseRepository.GetByIdAsync(id);

            if (course == null)
            {
                throw new Exception("Course not found");
            }

            var courseTypeRepository = _unitOfWork.GetRepository<CourseType, int>();
            var courseTypes = await courseTypeRepository.GetAllAsync();
            var courseType = courseTypes.FirstOrDefault(ct => ct.Name == courseDto.TypeName);

            if (courseType == null)
            {
                throw new Exception("CourseType not found");
            }
            course.TypeId = courseType.Id;

            var categoryRepository = _unitOfWork.GetRepository<Category, int>();
            var category = await categoryRepository.GetByIdAsync(courseDto.CategoryId);

            if (category == null)
            {
                throw new Exception("Category not found");
            }
            course.CategoryId = category.Id;
            _mapper.Map(courseDto, course);
            courseRepository.Update(course);

            await _unitOfWork.SaveChangesAsync();
            var resultDto = _mapper.Map<CourseDto>(course);

            return resultDto;
        }
    }
}
