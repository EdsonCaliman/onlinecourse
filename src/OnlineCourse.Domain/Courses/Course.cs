﻿using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Commons;
using OnlineCourse.Domain.Resources;

namespace OnlineCourse.Domain.Courses
{
    public class Course : Entity
    {
        public string Name { get; private set; }
        public double WorkLoad { get; private set; }
        public TargetAudience TargetAudience { get; private set; }
        public decimal Value { get; private set; }
        public string Description { get; private set; }
        public bool Concluded { get; set; }

        protected Course()
        {
        }

        public Course(string name, string _description, double workLoad, TargetAudience targetAudience, decimal value)
        {
            RuleValidator.New()
                .When(string.IsNullOrEmpty(name), Messages.INVALID_NAME)
                .When(workLoad < 1, Messages.INVALID_WORKLOAD)
                .When(value < 1, Messages.INVALID_VALUE)
                .ThrowExceptionIfExists();

            this.Name = name;
            this.WorkLoad = workLoad;
            this.TargetAudience = targetAudience;
            this.Value = value;
            this.Description = _description;
        }

        public void ChangeName(string name)
        {
            RuleValidator.New()
                .When(string.IsNullOrEmpty(name), Messages.INVALID_NAME)
                .ThrowExceptionIfExists();

            this.Name = name;
        }

        public void ChangeWorkLoad(double workLoad)
        {
            RuleValidator.New()
                .When(workLoad < 1, Messages.INVALID_WORKLOAD)
                .ThrowExceptionIfExists();

            this.WorkLoad = workLoad;
        }

        public void ChangeValue(decimal value)
        {
            RuleValidator.New()
                .When(value < 1, Messages.INVALID_VALUE)
                .ThrowExceptionIfExists();

            this.Value = value;
        }
    }
}
