using OnlineCourse.Domain.Courses;

namespace OnlineCourse.Domain._Base
{
    public interface ICourseRepository : IRepository<Course>
    {
        Course GetByName(string name);
    }
}
