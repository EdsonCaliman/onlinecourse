using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Students;
using OnlineCourse.Infra._Base;
using OnlineCourse.Infra.Contexts;
using System.Linq;

namespace OnlineCourse.Infra.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Student GetByIdentificationId(int identificationId)
        {
            var query = _context.Set<Student>().Where(s => s.IdentificationId == identificationId);
            return query.Any() ? query.First() : null;
        }
    }
}
