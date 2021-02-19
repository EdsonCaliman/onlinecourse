using Bogus;
using ExpectedObjects;
using OnlineCourse.Domain.Courses;
using OnlineCourse.DomainTests._Builders;
using OnlineCourse.DomainTests._Extentions;
using System;
using Xunit;
using Xunit.Abstractions;

namespace OnlineCourse.DomainTests.Courses
{
    public class CourseTest : IDisposable
    {
        private readonly string _name;
        private readonly string _description;
        private readonly double _workLoad;
        private readonly TargetAudience _targetAudience;
        private readonly double _value;
        private readonly ITestOutputHelper _output;

        public CourseTest(ITestOutputHelper output)
        {
            var faker = new Faker();
            _name = faker.Random.Word();
            _description = faker.Lorem.Paragraph();
            _workLoad = faker.Random.Double(50, 1000);
            _targetAudience = TargetAudience.Student;
            _value = faker.Random.Double(100, 1000); ;
            _output = output;
            _output.WriteLine("Construtor sendo executado");
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado");
        }


        [Fact]
        public void ShouldCreateCourse()
        {
            var expectedCourse = new
            {
                Name = _name,
                Description = _description,
                WorkLoad = _workLoad,
                TargetAudience = _targetAudience,
                Value = _value
            };

            var course = new Course(expectedCourse.Name, expectedCourse.Description, expectedCourse.WorkLoad,
                expectedCourse.TargetAudience, expectedCourse.Value);

            expectedCourse.ToExpectedObject().ShouldMatch(course);

        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotHaveCourseInvalidName(string invalidName)
        {
            Assert.Throws<ArgumentException>(() => CourseBuilder.New().WithName(invalidName).Build()).WithMessage("Invalid Name");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void ShouldNotHaveCourseWorkLoadLessThanOne(double invalidWorkLoad)
        {
            Assert.Throws<ArgumentException>(() => CourseBuilder.New().WithWorkLoad(invalidWorkLoad).Build()).WithMessage("Invalid WorkLoad");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void ShouldNotHaveCourseValueLessThanOne(double invalidValue)
        {
            Assert.Throws<ArgumentException>(() => CourseBuilder.New().WithValue(invalidValue).Build()).WithMessage("Invalid Value");
        }
    }
}
