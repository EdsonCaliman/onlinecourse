using Bogus;
using ExpectedObjects;
using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Commons;
using OnlineCourse.Domain.Students;
using OnlineCourse.DomainTests._Builders;
using Xunit;

namespace OnlineCourse.DomainTests.Students
{
    public class StudentTest
    {
        private readonly Faker _fake;

        public StudentTest()
        {
            _fake = new Faker();
        }

        [Fact]
        public void ShouldCreateStudent()
        {
            var expectedStudent = new
            {
                Name = _fake.Person.FullName,
                IdentificationId = _fake.Random.Int(),
                Email = _fake.Person.Email,
                TargetAudience = TargetAudience.Student
            };

            var student = new Student(expectedStudent.Name, expectedStudent.IdentificationId, expectedStudent.Email, expectedStudent.TargetAudience);

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
        [InlineData(0)]
        [InlineData(-10)]
        public void ShouldNotHaveStudentInvalidIdentificationId(int invalidIdentificationId)
        {
            Assert.Throws<DomainException>(() => StudentBuilder.New().WithIdentificationId(invalidIdentificationId).Build());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotHaveStudentInvalidEmail(string invalidEmail)
        {
            Assert.Throws<DomainException>(() => StudentBuilder.New().WithEmail(invalidEmail).Build());
        }
    }
}
