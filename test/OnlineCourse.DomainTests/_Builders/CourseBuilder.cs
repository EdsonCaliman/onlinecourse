using OnlineCourse.Domain.Commons;
using OnlineCourse.Domain.Courses;

namespace OnlineCourse.DomainTests._Builders
{
    public class CourseBuilder
    {
        private string _name  = "Basic Computing";
        private string _description = "Description";
        private double _workLoad = 80;
        private TargetAudience _targetAudience  = TargetAudience.Student;
        private decimal _value = 950;

        public static CourseBuilder New()
        {
            return new CourseBuilder();
        }

        public CourseBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CourseBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public CourseBuilder WithWorkLoad(double workLoad)
        {
            _workLoad = workLoad;
            return this;
        }

        public CourseBuilder WithValue(decimal value)
        {
            _value = value;
            return this;
        }

        public CourseBuilder WithTargetAudience(TargetAudience targetAudience)
        {
            _targetAudience = targetAudience;
            return this;
        }

        public Course Build()
        {
            return new Course(_name, _description, _workLoad, _targetAudience, _value);
        }
    }
}
