using Sahibinden.Core.Entities;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;

namespace Sahibinden.Core.EntityFramework
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter = null);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);

    }
}
