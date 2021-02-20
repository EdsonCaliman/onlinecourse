using System;
using System.Collections.Generic;

namespace OnlineCourse.Domain._Base
{
    [Serializable]
    public class DomainException : Exception
    {
        public List<string> ListOfRules { get; set; }

        public DomainException(List<string> listOfRules)
        {
            ListOfRules = listOfRules;
        }
    }
}