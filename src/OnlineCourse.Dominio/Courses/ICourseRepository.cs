namespace OnlineCourse.DomainTests.Courses
{
    public interface ICourseRepository
    {
        void Add(Course course);
        void Update(Course course);
        Course GetByName(string name);
    }


}
