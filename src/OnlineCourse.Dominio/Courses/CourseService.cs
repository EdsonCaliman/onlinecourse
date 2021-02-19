using System;

namespace OnlineCourse.DomainTests.Courses
{
    public class CourseService
    {
        private ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void Save(CourseDto courseDto)
        {
            var courseAlreadySave = _courseRepository.GetByName(courseDto.Name);

            if (courseAlreadySave != null)
            {
                throw new ArgumentException("This name is already exists");
            }

            if (!Enum.TryParse<TargetAudience>(courseDto.TargetAudience, out var targetAudience))
            {
                throw new ArgumentException("Invalid Target Audience");
            }

            var course = new Course(courseDto.Name, courseDto.Description, courseDto.WorkLoad,
                targetAudience, courseDto.Value);

            _courseRepository.Add(course);
        }
    }


}
