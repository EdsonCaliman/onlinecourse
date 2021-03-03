using OnlineCourse.Domain.Courses;
using OnlineCourse.Domain.Registrations;
using OnlineCourse.Domain.Students;

namespace OnlineCourse.DomainTests._Builders
{
    public class RegistrationBuilder
    {
        const decimal courseValue = 100;
        private Student Student = StudentBuilder.New().Build();
        private Course Course = CourseBuilder.New().WithValue(courseValue).Build();
        private decimal Value = courseValue - 1;

        public static RegistrationBuilder New()
        {
            return new RegistrationBuilder();
        }

        public RegistrationBuilder WithStudent(Student student)
        {
            Student = student;
            return this;
        }

        public RegistrationBuilder WithCourse(Course course)
        {
            Course = course;
            return this;
        }

        public RegistrationBuilder WithValue(decimal value)
        {
            Value = value;
            return this;
        }

        public Registration Build()
        {
            return new Registration(Student, Course, Value);
        }
    }
}
