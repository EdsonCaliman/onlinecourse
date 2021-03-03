using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Courses;
using OnlineCourse.Domain.Resources;
using OnlineCourse.Domain.Students;

namespace OnlineCourse.Domain.Registrations
{
    public class Registration
    {
        public Student Student { get; private set; }
        public Course Course { get; private set; }
        public decimal Value { get; private set; }
        public bool HasDiscount { get; private set; }

        public Registration(Student student, Course course, decimal value)
        {
            RuleValidator.New()
                .When(student == null, Messages.INVALID_STUDENT)
                .When(course == null, Messages.INVALID_COURSE)
                .When(value < 1, Messages.INVALID_VALUE)
                .When(course != null && value > course.Value, Messages.REGISTRATION_VALUE_SHOULD_NOT_BE_BIGGER_THAN_COURSE)
                .ThrowExceptionIfExists();

            Student = student;
            Course = course;
            Value = value;
            HasDiscount = value < course.Value;
        }
    }
}
