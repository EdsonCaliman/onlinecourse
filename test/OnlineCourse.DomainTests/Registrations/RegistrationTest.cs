using ExpectedObjects;
using OnlineCourse.Domain._Base;
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
                Value = course.Value
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

    }
}
