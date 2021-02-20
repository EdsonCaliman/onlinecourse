using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Domain.Courses;
using System;
using System.Collections.Generic;
using OnlineCourse.Domain._Base;

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
        public IEnumerable<CourseListDto> Get()
        {
            return _courseService.GetAll();
        }

        [HttpPost]
        public IActionResult Add(CourseDto model)
        {
            try
            {
                _courseService.Add(model);
            }
            catch (Exception ex)
            {
                if(ex is DomainException exception)
                {
                    return new
                        JsonResult(exception.ListOfRules);
                }

            }

            return Ok();
        }

        [HttpPut]
        public IActionResult Update(CourseDto model)
        {
            try
            {
                _courseService.Update(model);
            }
            catch (Exception ex)
            {
                if (ex is DomainException exception)
                {
                    return new
                        JsonResult(exception.ListOfRules);
                }

            }

            return Ok();
        }
    }
}
