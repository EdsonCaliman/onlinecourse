using Bogus;
using ExpectedObjects;
using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Commons;
using OnlineCourse.Domain.Courses;
using OnlineCourse.Domain.Resources;
using OnlineCourse.DomainTests._Builders;
using OnlineCourse.DomainTests._Extentions;
using Xunit;

namespace OnlineCourse.DomainTests.Courses
{
    public class CourseTest
    {
        private readonly Faker _faker;
        private readonly string _name;
        private readonly string _description;
        private readonly double _workLoad;
        private readonly TargetAudience _targetAudience;
        private readonly double _value;
  
        public CourseTest()
        {
            _faker = new Faker();
            _name = _faker.Random.Word();
            _description = _faker.Lorem.Paragraph();
            _workLoad = _faker.Random.Double(50, 1000);
            _targetAudience = TargetAudience.Student;
            _value = _faker.Random.Double(100, 1000); ;
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
            Assert.Throws<DomainException>(() => CourseBuilder.New().WithName(invalidName).Build()).WithMessage(Messages.INVALID_NAME);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void ShouldNotHaveCourseWorkLoadLessThanOne(double invalidWorkLoad)
        {
            Assert.Throws<DomainException>(() => CourseBuilder.New().WithWorkLoad(invalidWorkLoad).Build()).WithMessage(Messages.INVALID_WORKLOAD);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void ShouldNotHaveCourseValueLessThanOne(double invalidValue)
        {
            Assert.Throws<DomainException>(() => CourseBuilder.New().WithValue(invalidValue).Build()).WithMessage(Messages.INVALID_VALUE);
        }

        [Fact]
        public void ShouldChangeName()
        {
            var expectedName = _faker.Person.FullName;

            var course = CourseBuilder.New().Build();

            course.ChangeName(expectedName);

            Assert.Equal(expectedName, course.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotChangeInvalidName(string invalidName)
        {
            var course = CourseBuilder.New().Build();

            Assert.Throws<DomainException>(() => course.ChangeName(invalidName)).WithMessage(Messages.INVALID_NAME);
        }

        [Fact]
        public void ShouldChangeWorkLoad()
        {
            var expectedWorkLoad = 80;

            var course = CourseBuilder.New().Build();

            course.ChangeWorkLoad(expectedWorkLoad);

            Assert.Equal(expectedWorkLoad, course.WorkLoad);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void ShouldNotChangeInvalidWorkLoad(int invalidWorkLoad)
        {
            var course = CourseBuilder.New().Build();

            Assert.Throws<DomainException>(() => course.ChangeWorkLoad(invalidWorkLoad)).WithMessage(Messages.INVALID_WORKLOAD);
        }

        [Fact]
        public void ShouldChangeValue()
        {
            var expectedValue = 800;

            var course = CourseBuilder.New().Build();

            course.ChangeValue(expectedValue);

            Assert.Equal(expectedValue, course.Value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void ShouldNotChangeInvalidValue(double invalidValue)
        {
            var course = CourseBuilder.New().Build();

            Assert.Throws<DomainException>(() => course.ChangeValue(invalidValue)).WithMessage(Messages.INVALID_VALUE);
        }
    }
}
