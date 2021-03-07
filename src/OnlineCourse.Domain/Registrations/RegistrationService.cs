using OnlineCourse.Domain._Base;
using System.Collections.Generic;
using System.Linq;

namespace OnlineCourse.Domain.Registrations
{
    public class RegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationService(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public IEnumerable<RegistrationDto> GetAll()
        {
            var registrationList = _registrationRepository.GetAll();

            if (registrationList.Any())
            {
                var registrationDtoList = registrationList.Select(c => new RegistrationDto
                {
                    Id = c.Id,
                    CourseId = c.CourseId,
                    StudentId = c.StudentId,
                    Value = c.Value
                });

                return registrationDtoList;
            }

            return null;
        }
    }
}
