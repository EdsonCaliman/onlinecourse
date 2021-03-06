using Moq;
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
    public class RegistrationCreationTest
    {
        private readonly Mock<ICourseRepository> _courseRepository;
        private readonly Mock<IStudentRepository> _studentRepository;
        private readonly Mock<IRegistrationRepository> _registrationRepository;
        private readonly RegistrationCreation _registrationCreation;
        private readonly Course _course;
        private readonly RegistrationDto _registrationDto;
        private readonly Student _student;

        public RegistrationCreationTest()
        {
            _courseRepository = new Mock<ICourseRepository>();
            _studentRepository = new Mock<IStudentRepository>();
            _registrationRepository = new Mock<IRegistrationRepository>();
            _registrationCreation = new RegistrationCreation(_studentRepository.Object, _courseRepository.Object, _registrationRepository.Object);

            _course = CourseBuilder.New().WithTargetAudience(TargetAudience.Student).Build();
            _courseRepository.Setup(r => r.GetById(_course.Id)).Returns(_course);

            _student = StudentBuilder.New().WithTargetAudience(TargetAudience.Student).Build();
            _studentRepository.Setup(r => r.GetById(_student.Id)).Returns(_student);

            _registrationDto = new RegistrationDto { StudentId = _student.Id, CourseId = _course.Id, Value = _course.Value };
        }

        [Fact]
        public void ShouldNotCreateWithInvalidCourse()
        {
            Course invalidCourse = null;
            _courseRepository.Setup(r => r.GetById(_registrationDto.CourseId)).Returns(invalidCourse);

            Assert.Throws<DomainException>(() =>
                _registrationCreation.Create(_registrationDto))
                .WithMessage(Messages.INVALID_COURSE);
        }

        [Fact]
        public void ShouldNotCreateWithInvalidStudent()
        {
            Student student = null;
            _studentRepository.Setup(r => r.GetById(_registrationDto.StudentId)).Returns(student);

            Assert.Throws<DomainException>(() =>
                _registrationCreation.Create(_registrationDto))
                .WithMessage(Messages.INVALID_STUDENT);
        }

        [Fact]
        public void ShouldAddRegistration()
        {
            _registrationCreation.Create(_registrationDto);

            _registrationRepository.Verify(r => r.Add(It.Is<Registration>(m => m.Course == _course && m.Student == _student)));
        }

    }
}
