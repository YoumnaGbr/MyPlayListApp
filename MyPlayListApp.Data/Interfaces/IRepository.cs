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
        bool Delete(object id);
        bool Delete(Expression<Func<T, bool>> predicate);
        T Update(T entity);
        T GetById(Guid id);
        List<T> GetAll();
        bool Any(Expression<Func<T, bool>> predicate);

    }
}
