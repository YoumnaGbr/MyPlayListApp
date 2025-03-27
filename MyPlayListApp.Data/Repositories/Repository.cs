using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPlayListApp.Data.Context;
using MyPlayListApp.Data.Interfaces;

namespace MyPlayListApp.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MyPlayListAppContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(MyPlayListAppContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();  // Getting DbSet for the given entity type
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(object id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(Expression<Func<T, bool>> predicate)
        {
            T entityToDelete = _dbSet.FirstOrDefault(predicate);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
                return true;
            }
            return false;
        }

        public virtual T Update(T entity)
        {
            entity = _dbSet.Attach(entity).Entity;
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }
        public T GetById(Guid id)
        {
            return _context.Find<T>(id);
        }
        public List<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }
        public virtual bool Any(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }
    }
}
