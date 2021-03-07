using Microsoft.EntityFrameworkCore;
using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Registrations;
using OnlineCourse.Infra._Base;
using OnlineCourse.Infra.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace OnlineCourse.Infra.Repositories
{
    public class RegistrationRepository : BaseRepository<Registration>, IRegistrationRepository
    {

        public RegistrationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Registration GetByIdWithIncludes(int id)
        {
            var query = _context.Set<Registration>()
                .Where(entidade => entidade.Id == id)
                .Include(c => c.Course)
                .Include(s => s.Student);
            return query.Any() ? query.First() : null;
        }

    }
}
