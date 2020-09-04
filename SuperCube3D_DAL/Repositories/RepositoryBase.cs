using SuperCube3D_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCube3D_DAL.Repositories
{
    public abstract class RepositoryBase<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _ctx;
        private DbSet<TEntity> _dbSet;

        public RepositoryBase(DbContext context)
        {
            _ctx = context;
            _dbSet = _ctx.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity Get(int id)
        {
            return _dbSet.FirstOrDefault(entity => entity.Id.Equals(id));
        }

        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
            _ctx.SaveChanges();
        }

        public void Edit(TEntity entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            var entityToRemove = _dbSet.FirstOrDefault(entity => entity.Id.Equals(id));

            _dbSet.Remove(entityToRemove);
            _ctx.SaveChanges();
        }
    }
}
