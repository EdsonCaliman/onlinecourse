using OnlineCourse.Domain.Registrations;
using System.Collections.Generic;

namespace OnlineCourse.Domain._Base
{
    public interface IRegistrationRepository : IRepository<Registration>
    {
        Registration GetByIdWithIncludes(int id);
    }
}
