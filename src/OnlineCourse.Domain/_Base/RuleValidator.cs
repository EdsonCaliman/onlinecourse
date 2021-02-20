using System.Collections.Generic;
using System.Linq;

namespace OnlineCourse.Domain._Base
{
    public class RuleValidator
    {
        private readonly List<string> _listOfRules;

        private RuleValidator()
        {
            _listOfRules = new List<string>();
        }

        public static RuleValidator New()
        {
            return new RuleValidator();
        }

        public RuleValidator When(bool hasError, string message)
        {
            if (hasError)
            {
                _listOfRules.Add(message);
            }

            return this;
        }

        public void ThrowExceptionIfExists()
        {
            if (_listOfRules.Any())
            {
                throw new DomainException(_listOfRules);
            }
        }
    }
}
