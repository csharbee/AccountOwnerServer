using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByContition(Expression<Func<T, bool>> expression);
        void Create(T model);
        void Update(T model);
        void Delete(T model);
    }
}
