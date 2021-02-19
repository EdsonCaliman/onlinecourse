using System.Threading.Tasks;

namespace OnlineCourse.Infra._Base
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
