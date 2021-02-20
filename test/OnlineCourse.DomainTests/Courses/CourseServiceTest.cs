using Bogus;
using Moq;
using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Courses;
using OnlineCourse.Domain.Resources;
using OnlineCourse.DomainTests._Builders;
using OnlineCourse.DomainTests._Extentions;
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
                Id = 323,
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
            _courseService.Add(_courseDto);

            //_courseRepositoryMock.Verify(r => r.Add(It.IsAny<Course>()), Times.AtLeast(1));
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

            Assert.Throws<DomainException>(() => _courseService.Add(_courseDto))
                .WithMessage(Messages.INVALID_TARGETAUDIENCE);
        }

        [Fact]
        public void ShouldNotAddCourseWithSameName()
        {
            var courseAlreadySave = CourseBuilder.New().WithName(_courseDto.Name).Build();

            _courseRepositoryMock.Setup(r => r.GetByName(_courseDto.Name)).Returns(courseAlreadySave);

            Assert.Throws<DomainException>(() => _courseService.Add(_courseDto))
                .WithMessage(Messages.NAME_IS_ALREADY_EXISTS);

        }

        [Fact]
        public void ShouldUpdateCourse()
        {
            var courseAlreadySave = CourseBuilder.New().Build();

            _courseRepositoryMock.Setup(r => r.GetById(_courseDto.Id)).Returns(courseAlreadySave);

            _courseService.Update(_courseDto);

            Assert.Equal(_courseDto.Name, courseAlreadySave.Name);
            Assert.Equal(_courseDto.WorkLoad, courseAlreadySave.WorkLoad);
            Assert.Equal(_courseDto.Value, courseAlreadySave.Value);

            _courseRepositoryMock.Verify(r => r.Update(It.IsAny<Course>()));
            _courseRepositoryMock.Verify(r => r.Add(It.IsAny<Course>()), Times.Never());
        }

    }


}
