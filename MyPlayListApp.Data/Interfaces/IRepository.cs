using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayListApp.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
     
        T Add(T entity);
        void Delete(object id);
        void Delete(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, bool asNoTracking = false);
        T Update(T entity);
        T GetById(Guid id);
        List<T> GetAll();

    }
}
