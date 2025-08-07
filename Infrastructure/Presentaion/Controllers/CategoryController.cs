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
    public class CategoryController(IServiceManager _serviceManager) :BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _serviceManager.CategoryServices.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _serviceManager.CategoryServices.GetCategoryByIdAsync(id);
            if (category is null)
                return NotFound($"Category with ID {id} not found.");
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _serviceManager.CategoryServices.CreateCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _serviceManager.CategoryServices.UpdateCategoryAsync(id, categoryDto);
            if (updated is null)
                return NotFound($"Category with ID {id} not found.");
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var success = await _serviceManager.CategoryServices.DeleteCategoryAsync(id);
            if (!success)
                return NotFound($"Category with ID {id} not found.");
            return NoContent();
        }
    }
}
