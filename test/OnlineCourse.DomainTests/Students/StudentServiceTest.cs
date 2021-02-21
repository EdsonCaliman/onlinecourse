using Bogus;
using Moq;
using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Resources;
using OnlineCourse.Domain.Students;
using OnlineCourse.DomainTests._Builders;
using OnlineCourse.DomainTests._Extentions;
using Xunit;

namespace OnlineCourse.DomainTests.Students
{
    public class StudentServiceTest
    {
        private readonly Faker _faker;
        private readonly StudentDto _studentDto;
        private readonly Mock<IStudentRepository> _studentRepositoryMock;
        private readonly StudentService _studentService;

        public StudentServiceTest()
        {
            _faker = new Faker();
            _studentDto = new StudentDto
            {
                Name = _faker.Person.FullName,
                Email = _faker.Person.Email,
                IdentificationId = _faker.Random.Int(1, 1000),
                TargetAudience = "Student"
            };

            _studentRepositoryMock = new Mock<IStudentRepository>();
            _studentService = new StudentService(_studentRepositoryMock.Object);

        }

        [Fact]
        public void ShouldAddStudent()
        {
            _studentService.Add(_studentDto);

            _studentRepositoryMock.Verify(s => s.Add(It.IsAny<Student>()));
            
        }

        [Fact]
        public void ShouldNotAddStudentWithSameIdentificationId()
        {
            var studentAlreadySave = StudentBuilder.New().WithName(_studentDto.Name).Build();

            _studentRepositoryMock.Setup(r => r.GetByIdentificationId(_studentDto.IdentificationId)).Returns(studentAlreadySave);

            Assert.Throws<DomainException>(() => _studentService.Add(_studentDto))
                .WithMessage(Messages.IDENTIFICATION_ID_IS_ALREADY_EXISTS);

        }

        [Fact]
        public void ShouldUpdateStudent()
        {
            var studentAlreadySave = StudentBuilder.New().Build();

            _studentRepositoryMock.Setup(r => r.GetById(_studentDto.Id)).Returns(studentAlreadySave);

            _studentService.Update(_studentDto);

            Assert.Equal(_studentDto.Name, studentAlreadySave.Name);

            _studentRepositoryMock.Verify(r => r.Update(It.IsAny<Student>()));
            _studentRepositoryMock.Verify(r => r.Add(It.IsAny<Student>()), Times.Never());
        }

        [Fact]
        public void ShouldNotAddWithInvalidTargetAudience()
        {
            var invalidTargetAudience = "Doctor";
            _studentDto.TargetAudience = invalidTargetAudience;

            Assert.Throws<DomainException>(() => _studentService.Add(_studentDto))
                .WithMessage(Messages.INVALID_TARGETAUDIENCE);
        }
    }
}
