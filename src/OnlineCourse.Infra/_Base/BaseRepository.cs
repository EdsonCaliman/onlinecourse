using OnlineCourse.Domain._Base;
using OnlineCourse.Infra.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace OnlineCourse.Infra._Base
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public List<TEntity> GetAll()
        {
            var entities = _context.Set<TEntity>().ToList();
            return entities.Any() ? entities : new List<TEntity>();
        }

        public TEntity GetById(int id)
        {
            var query = _context.Set<TEntity>().Where(entidade => entidade.Id == id);
            return query.Any() ? query.First() : null;
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
