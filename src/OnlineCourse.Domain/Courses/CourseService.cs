using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Commons;
using OnlineCourse.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineCourse.Domain.Courses
{
    public class CourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public IEnumerable<CourseListDto> GetAll()
        {
            var courseList = _courseRepository.GetAll();

            if (courseList.Any())
            {
                var courseDtoList = courseList.Select(c => new CourseListDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    TargetAudience = c.TargetAudience.ToString(),
                    Value = c.Value,
                    WorkLoad = c.WorkLoad
                });

                return courseDtoList;
            }

            return null;
        }

        public void Add(CourseDto courseDto)
        {
            var courseAlreadySave = _courseRepository.GetByName(courseDto.Name);

            RuleValidator.New()
                .When(courseAlreadySave != null, Messages.NAME_IS_ALREADY_EXISTS)
                .When(!Enum.TryParse<TargetAudience>(courseDto.TargetAudience, out var targetAudience), Messages.INVALID_TARGETAUDIENCE)
                .ThrowExceptionIfExists();

            var course = new Course(courseDto.Name, courseDto.Description, courseDto.WorkLoad,
                targetAudience, courseDto.Value);

            _courseRepository.Add(course);
           
        }

        public void Update(CourseDto courseDto)
        {
            var courseAlreadySave = _courseRepository.GetById(courseDto.Id);

            if (courseAlreadySave == null)
            {
                return;
            }

            courseAlreadySave.ChangeName(courseDto.Name);
            courseAlreadySave.ChangeWorkLoad(courseDto.WorkLoad);
            courseAlreadySave.ChangeValue(courseDto.Value);

            _courseRepository.Update(courseAlreadySave);

        }
    }


}
