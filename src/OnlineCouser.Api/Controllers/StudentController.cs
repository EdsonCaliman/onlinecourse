﻿using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Students;
using System;
using System.Collections.Generic;

namespace OnlineCouser.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IEnumerable<StudentListDto> Get()
        {
            return _studentService.GetAll();
        }

        [HttpPost]
        public IActionResult Add(StudentDto model)
        {
            try
            {
                _studentService.Add(model);
            }
            catch (DomainException exception)
            {
                return new JsonResult(exception.ListOfRules);

            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

            return Ok();
        }

        [HttpPut]
        public IActionResult Update(StudentDto model)
        {
            try
            {
                _studentService.Update(model);
            }
            catch (DomainException exception)
            {
                return new JsonResult(exception.ListOfRules);

            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

            return Ok();
        }
    }
}
