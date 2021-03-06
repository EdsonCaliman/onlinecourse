using ExpectedObjects;
using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Commons;
using OnlineCourse.Domain.Courses;
using OnlineCourse.Domain.Registrations;
using OnlineCourse.Domain.Resources;
using OnlineCourse.Domain.Students;
using OnlineCourse.DomainTests._Builders;
using OnlineCourse.DomainTests._Extentions;
using Xunit;

namespace OnlineCourse.DomainTests.Registrations
{
    public class RegistrationTest
    {
        [Fact]
        public void ShouldAddRegistration()
        {
            var course = CourseBuilder.New().Build();

            var expectedRegistration = new
            {
                Student = StudentBuilder.New().Build(),
                Course = course,
                course.Value
            };

            var registration = new Registration(expectedRegistration.Student, expectedRegistration.Course, expectedRegistration.Value);

            expectedRegistration.ToExpectedObject().ShouldMatch(registration);
           
        }

        [Fact]
        public void ShouldNotCreateRegistrationWithoutStudent()
        {
            Student invalidStudent = null;

            Assert.Throws<DomainException>(() => RegistrationBuilder.New().WithStudent(invalidStudent).Build()).WithMessage(Messages.INVALID_STUDENT);
        }

        [Fact]
        public void ShouldNotCreateRegistrationWithoutCourse()
        {
            Course invalidCourse = null;

            Assert.Throws<DomainException>(() => RegistrationBuilder.New().WithCourse(invalidCourse).Build()).WithMessage(Messages.INVALID_COURSE);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void ShouldNotCreateRegistrationWithInvalidValue(decimal invalidValue)
        {
            Assert.Throws<DomainException>(() => RegistrationBuilder.New().WithValue(invalidValue).Build()).WithMessage(Messages.INVALID_VALUE);
        }

        [Fact]
        public void ShouldNotCreateRegistrationWithValueBiggerThanCourseValue()
        {
            var course = CourseBuilder.New().WithValue(100).Build();

            var valueBiggerThanCourse = course.Value + 1;

            Assert.Throws<DomainException>(() => RegistrationBuilder.New().WithCourse(course).WithValue(valueBiggerThanCourse).Build())
                .WithMessage(Messages.REGISTRATION_VALUE_SHOULD_NOT_BE_BIGGER_THAN_COURSE);

        }

        [Fact]
        public void ShouldIndicateThatThereWasDiscountInRegistration()
        {
            var course = CourseBuilder.New().WithValue(100).Build();

            var valuePaidWithDiscount = course.Value - 1;

            var registration = RegistrationBuilder.New().WithCourse(course).WithValue(valuePaidWithDiscount).Build();

            Assert.True(registration.HasDiscount);

        }

        [Fact]
        public void ShouldNotCreateWithTargetAudienceIsDiferent()
        {
            var course = CourseBuilder.New().WithTargetAudience(TargetAudience.Employee).Build();
            var student = StudentBuilder.New().WithTargetAudience(TargetAudience.Student).Build();

            Assert.Throws<DomainException>(() =>
                RegistrationBuilder.New().WithCourse(course).WithStudent(student).Build())
                .WithMessage(Messages.TARGET_AUDIENCE_IS_DIFERENT_STUDENT_COURSE);

        }

        [Fact]
        public void ShouldInformTheGradeOfTheStudent()
        {
            const double expectedGrade = 9.5;
            var registration = RegistrationBuilder.New().Build();

            registration.InformGrade(expectedGrade);

            Assert.Equal(expectedGrade, registration.StudentGrade);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        public void ShouldNotInformInvalidGrade(double invalidGrade)
        {
            var registration = RegistrationBuilder.New().Build();

            Assert.Throws<DomainException>(() =>
                registration.InformGrade(invalidGrade))
                .WithMessage(Messages.INVALID_GRADE_STUDENT);
        }

        [Fact]
        public void ShouldIndicateThatCourseWasConcluded()
        {
            const double expectedGrade = 9.5;
            var registration = RegistrationBuilder.New().Build();

            registration.InformGrade(expectedGrade);

            Assert.True(registration.Course.Concluded);
        }

        [Fact]
        public void ShouldCancelRegistration()
        {
            var registration = RegistrationBuilder.New().Build();

            registration.Cancel();

            Assert.True(registration.Canceled);
        }

        [Fact]
        public void ShouldNotInformGradeWhenRegistrationIsCanceled()
        {
            const double studentGrade = 3;
            var registration = RegistrationBuilder.New().Build();
            registration.Cancel();

            Assert.Throws<DomainException>(() =>
                registration.InformGrade(studentGrade))
                .WithMessage(Messages.CANCELED_REGISTRATION);

        }

        [Fact]
        public void ShouldNotCancelWhenRegistrationIsConcluded()
        {
            const double studentGrade = 3;
            var registration = RegistrationBuilder.New().Build();

            registration.InformGrade(studentGrade);

            Assert.Throws<DomainException>(() =>
                registration.Cancel())
                .WithMessage(Messages.CONCLUDED_REGISTRATION);
        }
    }
}
