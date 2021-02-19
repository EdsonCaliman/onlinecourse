using System.Collections.Generic;

namespace OnlineCourse.Domain._Base
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        TEntity GetById(int id);
        List<TEntity> GetAll();
    }

}
