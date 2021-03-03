using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Commons;
using OnlineCourse.Domain.Resources;

namespace OnlineCourse.Domain.Students
{
    public class Student : Entity
    {
        public string Name { get; private set; }
        public int IdentificationId { get; private set; }
        public string Email { get; private set; }
        public TargetAudience TargetAudience { get; private set; }

        private Student() { }

        public Student(string name, int identificationId, string email, TargetAudience targetAudiente)
        {

            RuleValidator.New()
                .When(string.IsNullOrEmpty(name), Messages.INVALID_NAME)
                .When(identificationId < 1, Messages.INVALID_IDENTIFICATION_ID)
                .When(string.IsNullOrEmpty(email), Messages.INVALID_EMAIL)
                .ThrowExceptionIfExists();

            Name = name;
            IdentificationId = identificationId;
            Email = email;
            TargetAudience = targetAudiente;
        }

        public void ChangeName(string name)
        {
            RuleValidator.New()
                .When(string.IsNullOrEmpty(name), Messages.INVALID_NAME)
                .ThrowExceptionIfExists();

            this.Name = name;
        }
    }
}
