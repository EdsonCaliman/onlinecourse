using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Courses;
using OnlineCourse.Domain.Resources;
using OnlineCourse.Domain.Students;

namespace OnlineCourse.Domain.Registrations
{
    public class Registration : Entity
    {
        public Student Student { get; private set; }
        public int StudentId { get; private set; }
        public Course Course { get; private set; }
        public int CourseId { get; private set; }
        public decimal Value { get; private set; }
        public bool HasDiscount { get; private set; }
        public double  StudentGrade { get; private set; }
        public bool Canceled { get; set; }

        public Registration(Student student, Course course, decimal value)
        {
            RuleValidator.New()
                .When(student == null, Messages.INVALID_STUDENT)
                .When(course == null, Messages.INVALID_COURSE)
                .When(value < 1, Messages.INVALID_VALUE)
                .When(course != null && value > course.Value, Messages.REGISTRATION_VALUE_SHOULD_NOT_BE_BIGGER_THAN_COURSE)
                .When(student != null && course != null && student.TargetAudience != course.TargetAudience, Messages.TARGET_AUDIENCE_IS_DIFERENT_STUDENT_COURSE)
                .ThrowExceptionIfExists();

            Student = student;
            Course = course;
            Value = value;
            HasDiscount = value < course.Value;
        }
        private Registration()
        {
        }

        public void InformGrade(double grade)
        {
            RuleValidator.New()
                .When(grade < 0 || grade > 10, Messages.INVALID_GRADE_STUDENT)
                .When(Canceled, Messages.CANCELED_REGISTRATION)
                .ThrowExceptionIfExists();

            StudentGrade = grade;
            Course.Concluded = true;
        }

        public void Cancel()
        {
            RuleValidator.New()
                .When(Course.Concluded, Messages.CONCLUDED_REGISTRATION)
                .ThrowExceptionIfExists();

            Canceled = true;
        }
    }
}
