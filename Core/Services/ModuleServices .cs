using AutoMapper;
using Domain.Contract;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using ServicesAbstraction;
using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ModuleServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IModuleServices
    {
        private readonly IGenericRepository<Module, int> _moduleRepo = _unitOfWork.GetRepository<Module, int>();

        public async Task<IEnumerable<ModuleDto>> GetModulesByCourseIdAsync(int courseId)
        {
            var modules = await _moduleRepo.FindByConditionAsync(m => m.CourseId == courseId);
            return _mapper.Map<IEnumerable<ModuleDto>>(modules);
        }

        public async Task<ModuleDto> GetModuleByIdAsync(int id)
        {
            var module = await _moduleRepo.GetByIdAsync(id);
            if (module == null) throw new Exception("Module not found");

            return _mapper.Map<ModuleDto>(module);
        }

        public async Task<ModuleDto> CreateModuleAsync(ModuleDto moduleDto)
        {
            var module = _mapper.Map<Module>(moduleDto);
            await _moduleRepo.AddAysnc(module);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ModuleDto>(module);
        }

        public async Task<ModuleDto> UpdateModuleAsync(int id, ModuleDto moduleDto)
        {
            var existingModule = await _moduleRepo.GetByIdAsync(id);
            if (existingModule == null) throw new Exception("Module not found");

            _mapper.Map(moduleDto, existingModule); // Update fields
            _moduleRepo.Update(existingModule);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ModuleDto>(existingModule);
        }

        public async Task<bool> DeleteModuleAsync(int id)
        {
            var module = await _moduleRepo.GetByIdAsync(id);
            if (module == null) return false;

            _moduleRepo.Remove(module);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}