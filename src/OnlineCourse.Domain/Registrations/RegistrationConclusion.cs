using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Resources;

namespace OnlineCourse.Domain.Registrations
{
    public class RegistrationConclusion
    {
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationConclusion(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public void Conclude(int registrationId, int expectedGrade)
        {
            var registration = _registrationRepository.GetById(registrationId);

            RuleValidator.New()
                .When(registration == null, Messages.INVALID_REGISTRATION)
                .ThrowExceptionIfExists();

            registration.InformGrade(expectedGrade);

        }
    }
}
