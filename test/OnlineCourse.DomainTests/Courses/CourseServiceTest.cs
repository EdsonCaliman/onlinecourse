using Bogus;
using Moq;
using OnlineCourse.DomainTests._Extentions;
using System;
using Xunit;

namespace OnlineCourse.DomainTests.Courses
{
    public class CourseServiceTest
    {
        private readonly CourseDto _courseDto;
        private readonly Mock<ICourseRepository> _courseRepositoryMock;
        private readonly CourseService _courseService;

        public CourseServiceTest()
        {
            var fake = new Faker();
            _courseDto = new CourseDto
            {
                Name = fake.Random.Words(),
                Description = fake.Lorem.Paragraph(),
                WorkLoad = fake.Random.Double(50, 1000),
                TargetAudience = "Student",
                Value = fake.Random.Double(1000, 2000)
            };

            _courseRepositoryMock = new Mock<ICourseRepository>();
            _courseService = new CourseService(_courseRepositoryMock.Object);
        }

        [Fact]
        public void ShouldAddCourse()
        {
            _courseService.Save(_courseDto);

            //courseStorageMock.Verify(r => r.Add(It.IsAny<Course>()), Times.AtLeast(1));
            _courseRepositoryMock.Verify(r => r.Add(
                It.Is<Course>(
                    c => c.Name == _courseDto.Name &&
                    c.Description == _courseDto.Description
                )));

        }

        [Fact]
        public void ShouldNotAddWithInvalidTargetAudience()
        {
            var invalidTargetAudience = "Doctor";
            _courseDto.TargetAudience = invalidTargetAudience;

            Assert.Throws<ArgumentException>(() => _courseService.Save(_courseDto))
                .WithMessage("Invalid Target Audience");
        }

        [Fact]
        public void ShouldNotAddCourseWithSameName()
        {
            var courseAlreadySave = CourseBuilder.New().WithName(_courseDto.Name).Build();

            _courseRepositoryMock.Setup(r => r.GetByName(_courseDto.Name)).Returns(courseAlreadySave);

            Assert.Throws<ArgumentException>(() => _courseService.Save(_courseDto))
                .WithMessage("This name is already exists");

        }

    }


}
