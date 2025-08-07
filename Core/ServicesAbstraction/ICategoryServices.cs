using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
     public interface ICategoryServices
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto?> GetCategoryByIdAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto);
        Task<CategoryDto?> UpdateCategoryAsync(int id, CategoryDto categoryDto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
