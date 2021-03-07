using Bogus;
using ExpectedObjects;
using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Commons;
using OnlineCourse.Domain.Resources;
using OnlineCourse.Domain.Students;
using OnlineCourse.DomainTests._Builders;
using OnlineCourse.DomainTests._Extentions;
using Xunit;

namespace OnlineCourse.DomainTests.Students
{
    public class StudentTest
    {
        private readonly Faker _faker;

        public StudentTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void ShouldCreateStudent()
        {
            var expectedStudent = new
            {
                Name = _faker.Person.FullName,
                _faker.Person.Email,
                TargetAudience = TargetAudience.Student
            };

            var student = new Student(expectedStudent.Name, expectedStudent.Email, expectedStudent.TargetAudience);

            expectedStudent.ToExpectedObject().ShouldMatch(student);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotHaveStudentInvalidName(string invalidName)
        {
            Assert.Throws<DomainException>(() => StudentBuilder.New().WithName(invalidName).Build());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotHaveStudentInvalidEmail(string invalidEmail)
        {
            Assert.Throws<DomainException>(() => StudentBuilder.New().WithEmail(invalidEmail).Build());
        }

        [Fact]
        public void ShouldChangeName()
        {
            var expectedName = _faker.Person.FullName;

            var student = StudentBuilder.New().Build();

            student.ChangeName(expectedName);

            Assert.Equal(expectedName, student.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotChangeInvalidName(string invalidName)
        {
            var student = StudentBuilder.New().Build();

            Assert.Throws<DomainException>(() => student.ChangeName(invalidName)).WithMessage(Messages.INVALID_NAME);
        }
    }
}
