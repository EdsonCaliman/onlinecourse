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
            var dto = new CourseDto
            {
                Name = "AA",
                Description = "AA",
                TargetAudience = "Student",
                Value = 1000,
                WorkLoad = 80
            };

            _courseService.Save(dto);

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
