using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibDatabase.Interface
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Edit(T entity);
        void Delete(T entity);
        T GetById(int id);
        IQueryable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
    }
 }
