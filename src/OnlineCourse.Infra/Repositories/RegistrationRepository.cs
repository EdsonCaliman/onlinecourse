using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Registrations;
using OnlineCourse.Infra._Base;
using OnlineCourse.Infra.Contexts;

namespace OnlineCourse.Infra.Repositories
{
    public class RegistrationRepository : BaseRepository<Registration>, IRegistrationRepository
    {

        public RegistrationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
