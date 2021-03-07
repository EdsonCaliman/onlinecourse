using Moq;
using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Registrations;
using OnlineCourse.Domain.Resources;
using OnlineCourse.DomainTests._Builders;
using OnlineCourse.DomainTests._Extentions;
using Xunit;

namespace OnlineCourse.DomainTests.Registrations
{
    public class RegistrationConclusionTest
    {
        private readonly Domain.Registrations.Registration _registration;
        private readonly Mock<IRegistrationRepository> _registrationRepository;
        private readonly RegistrationConclusion _registrationConclusion;

        public RegistrationConclusionTest()
        {
            _registration = RegistrationBuilder.New().Build();
            _registrationRepository = new Mock<IRegistrationRepository>();
            _registrationConclusion = new RegistrationConclusion(_registrationRepository.Object);
        }

        [Fact]
        public void ShouldInforStudentGrade()
        {
            var expectedStudentGrade = 8;
            _registrationRepository.Setup(r => r.GetByIdWithIncludes(_registration.Id)).Returns(_registration);

            _registrationConclusion.Conclude(_registration.Id, expectedStudentGrade);

            Assert.Equal(expectedStudentGrade, _registration.StudentGrade);
        }

        [Fact]
        public void ShouldNotCreatWithInvalidRegistration()
        {
            var expectedStudentGrade = 8;
            Registration invalidRegistration = null;
            _registrationRepository.Setup(r => r.GetById(_registration.Id)).Returns(invalidRegistration);

            Assert.Throws<DomainException>(() =>
                _registrationConclusion.Conclude(_registration.Id, expectedStudentGrade))
                .WithMessage(Messages.INVALID_REGISTRATION);
        }
    }

}
