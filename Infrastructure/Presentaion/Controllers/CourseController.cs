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
    public class CourseController(IServiceManager _serviceManager) : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCouses()
        {
            var Products =await _serviceManager.CourseServices.GetAllCoursesAsync();    
            return Ok(Products);    
        }
        [HttpGet ("{id:int}")]
        public async Task<ActionResult> GetCouse(int id) 
        {
            var Product =await _serviceManager.CourseServices.GetCourseByIdAsync(id);
            return Ok(Product);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllType() 
        {
            var Types =await _serviceManager.CourseServices.GetAllTypeAsync();
            return Ok(Types);
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> AddCourse(CourseDto course)
        {
            var Couse =await _serviceManager.CourseServices.AddCourseAsync(course);
            return Ok(Couse);   
        }

        [HttpPut]
        public async Task<ActionResult<CourseDto>> UpdateCourse( int id, CourseDto course)
        {
            var UpdateCourse =await _serviceManager.CourseServices.UpdateCourseAsync(id, course);    
            return Ok(UpdateCourse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCourse(int id) 
        {
            var Delete =await _serviceManager.CourseServices.DeleteCourseAsync(id);
            return Ok(Delete);
        }

    }
}
