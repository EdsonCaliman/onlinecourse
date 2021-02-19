using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Courses;
using OnlineCourse.Infra._Base;
using OnlineCourse.Infra.Contexts;
using System.Linq;

namespace OnlineCourse.Infra.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context): base(context)
        {
        }

        public Course GetByName(string name)
        {
            var query = _context.Set<Course>().Where(course => course.Name.Contains(name));
            return query.Any() ? query.First() : null;
        }
    }
}
