using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    public class ModuleController(IServiceManager _serviceManager) : BaseController
    {
        [HttpGet("ByCourse/{courseId:int}")]
        public async Task<ActionResult> GetModulesByCourseId(int courseId)
        {
            var modules = await _serviceManager.ModuleServices.GetModulesByCourseIdAsync(courseId);
            return Ok(modules);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetModuleById(int id)
        {
            var module = await _serviceManager.ModuleServices.GetModuleByIdAsync(id);
            return Ok(module);
        }

        [HttpPost]
        public async Task<ActionResult> CreateModule([FromBody] ModuleDto moduleDto)
        {
            var created = await _serviceManager.ModuleServices.CreateModuleAsync(moduleDto);
            return CreatedAtAction(nameof(GetModuleById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateModule(int id, [FromBody] ModuleDto moduleDto)
        {
            var updated = await _serviceManager.ModuleServices.UpdateModuleAsync(id, moduleDto);
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteModule(int id)
        {
            var deleted = await _serviceManager.ModuleServices.DeleteModuleAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}