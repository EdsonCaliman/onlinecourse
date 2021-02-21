using Bogus;
using OnlineCourse.Domain.Commons;
using OnlineCourse.Domain.Students;

namespace OnlineCourse.DomainTests._Builders
{
    public class StudentBuilder
    {
        private readonly Faker _fake;

        private string Name;
        private int IdentificationId;
        private string Email;
        private TargetAudience TargetAudience;
        private StudentBuilder()
        {
            _fake = new Faker();
            Name = _fake.Person.FullName;
            IdentificationId = _fake.Random.Int();
            Email = _fake.Person.Email;
            TargetAudience = TargetAudience.Student;
        }

        public static StudentBuilder New()
        {
            return new StudentBuilder();
        }

        public StudentBuilder WithName(string name)
        {
            this.Name = name;

            return this;
        }

        public StudentBuilder WithIdentificationId(int identificationId)
        {
            this.IdentificationId = identificationId;

            return this;
        }

        public StudentBuilder WithEmail(string email)
        {
            this.Email = email;

            return this;
        }

        public Student Build()
        {
            return new Student(Name, IdentificationId, Email, TargetAudience);
        }
    }
}
