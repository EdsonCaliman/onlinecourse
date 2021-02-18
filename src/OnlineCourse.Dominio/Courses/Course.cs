﻿using System;

namespace OnlineCourse.DomainTests.Courses
{
    public class Course
    {
        public string Name { get; private set; }
        public double WorkLoad { get; private set; }
        public TargetAudience TargetAudience { get; private set; }
        public double Value { get; private set; }

        public string Description { get; private set; }

        public Course(string name, string _description, double workLoad, TargetAudience targetAudience, double value)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Invalid Name");
            }

            if (workLoad < 1)
            {
                throw new ArgumentException("Invalid WorkLoad");
            }

            if (value < 1)
            {
                throw new ArgumentException("Invalid Value");
            }

            this.Name = name;
            this.WorkLoad = workLoad;
            this.TargetAudience = targetAudience;
            this.Value = value;
            this.Description = _description;
        }
    }
}
