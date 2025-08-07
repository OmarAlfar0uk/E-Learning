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
    public class CategoryServices(IUnitOfWork _unitOfWork, IMapper _mapper) : ICategoryServices
    {
        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var repository = _unitOfWork.GetRepository<Category, int>();
            var categories = await repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<Category, int>();
            var category = await repository.GetByIdAsync(id);
            return category is null ? null : _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto)
        {
            var repository = _unitOfWork.GetRepository<Category, int>();
            var category = _mapper.Map<Category>(categoryDto);
            await repository.AddAysnc(category);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(int id, CategoryDto categoryDto)
        {
            var repository = _unitOfWork.GetRepository<Category, int>();
            var existing = await repository.GetByIdAsync(id);
            if (existing is null) return null;

            existing.Name = categoryDto.Name;
            repository.Update(existing);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(existing);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<Category, int>();
            var category = await repository.GetByIdAsync(id);
            if (category is null) return false;

            repository.Remove(category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}