using OnlineCourse.Domain.Students;

namespace OnlineCourse.Domain._Base
{
    public interface IStudentRepository : IRepository<Student>
    {
        Student GetByIdentificationId(int identificationId);
    }
}
