using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Domain.Courses;
using System.Collections.Generic;

namespace OnlineCouser.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : Controller
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return _courseService.GetAll();
        }

        [HttpPost]
        public IActionResult Save(CourseDto model)
        {
            _courseService.Save(model);

            return Ok();
        }
    }
}
