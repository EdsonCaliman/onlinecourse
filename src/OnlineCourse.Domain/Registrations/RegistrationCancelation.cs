using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Resources;

namespace OnlineCourse.Domain.Registrations
{
    public class RegistrationCancelation
    {
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationCancelation(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public void Cancel(int registrationId)
        {
            var registration = _registrationRepository.GetByIdWithIncludes(registrationId);

            RuleValidator.New()
                .When(registration == null, Messages.INVALID_REGISTRATION)
                .ThrowExceptionIfExists();

            registration.Cancel();
        }
    }
}
