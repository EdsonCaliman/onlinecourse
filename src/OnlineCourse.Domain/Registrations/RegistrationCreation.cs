using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Resources;

namespace OnlineCourse.Domain.Registrations
{
    public class RegistrationCreation
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationCreation(IStudentRepository studentRepository, ICourseRepository courseRepository, IRegistrationRepository registrationRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _registrationRepository = registrationRepository;
        }

        public void Create(RegistrationDto registrationDto)
        {
            var student = _studentRepository.GetById(registrationDto.StudentId);
            var course = _courseRepository.GetById(registrationDto.CourseId);

            RuleValidator.New()
                .When(course == null, Messages.INVALID_COURSE)
                .When(student == null, Messages.INVALID_STUDENT)
                .ThrowExceptionIfExists();

            var registration = new Registration(student, course, registrationDto.Value);

            _registrationRepository.Add(registration);
        }
    }
}
