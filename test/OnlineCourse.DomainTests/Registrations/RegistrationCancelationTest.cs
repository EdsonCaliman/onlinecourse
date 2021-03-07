using Moq;
using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Registrations;
using OnlineCourse.Domain.Resources;
using OnlineCourse.DomainTests._Builders;
using OnlineCourse.DomainTests._Extentions;
using Xunit;

namespace OnlineCourse.DomainTests.Registrations
{
    public class RegistrationCancelationTest
    {
        private readonly Registration _registration;
        private readonly Mock<IRegistrationRepository> _registrationRepository;
        private readonly RegistrationCancelation _registrationCancelation;

        public RegistrationCancelationTest()
        {
            _registration = RegistrationBuilder.New().Build();
            _registrationRepository = new Mock<IRegistrationRepository>();
            _registrationCancelation = new RegistrationCancelation(_registrationRepository.Object);
        }

        [Fact]
        public void ShouldCancelRegistration()
        {
            _registrationRepository.Setup(r => r.GetByIdWithIncludes(_registration.Id)).Returns(_registration);

            _registrationCancelation.Cancel(_registration.Id);

            Assert.True(_registration.Canceled);
        }

        [Fact]
        public void ShouldNotCreatWithInvalidRegistration()
        {
            Registration invalidRegistration = null;
            _registrationRepository.Setup(r => r.GetById(_registration.Id)).Returns(invalidRegistration);

            Assert.Throws<DomainException>(() =>
                _registrationCancelation.Cancel(_registration.Id))
                .WithMessage(Messages.INVALID_REGISTRATION);
        }

    }
}
