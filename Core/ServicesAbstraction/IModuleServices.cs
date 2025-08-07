using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IModuleServices
    {
        Task<IEnumerable<ModuleDto>> GetModulesByCourseIdAsync(int courseId);
        Task<ModuleDto> GetModuleByIdAsync(int id);
        Task<ModuleDto> CreateModuleAsync(ModuleDto moduleDto);
        Task<ModuleDto> UpdateModuleAsync(int id, ModuleDto moduleDto);
        Task<bool> DeleteModuleAsync(int id);
    }
}
