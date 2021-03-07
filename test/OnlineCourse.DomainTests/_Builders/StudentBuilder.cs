using Bogus;
using OnlineCourse.Domain.Commons;
using OnlineCourse.Domain.Students;

namespace OnlineCourse.DomainTests._Builders
{
    public class StudentBuilder
    {
        private readonly Faker _faker;

        private string Name;
        private string Email;
        private TargetAudience TargetAudience;
        private StudentBuilder()
        {
            _faker = new Faker();
            Name = _faker.Person.FullName;
            Email = _faker.Person.Email;
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

        public StudentBuilder WithEmail(string email)
        {
            this.Email = email;

            return this;
        }

        public StudentBuilder WithTargetAudience(TargetAudience targetAudience)
        {
            this.TargetAudience = targetAudience;

            return this;
        }

        public Student Build()
        {
            return new Student(Name, Email, TargetAudience);
        }
    }
}
