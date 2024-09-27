using Sahibinden.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Abstract
{
    public interface IUserService
    {
        List<User> GetUsersAll();
        IQueryable<User> GetQueryable(bool status);
        IQueryable<User> GetQueryable(Expression<Func<User, bool>> filter = null);
        IQueryable<User> GetQueryableSearch();
        User GetById(int id);
        User Add(User user);
        User Update(User user);
        void UpdateDeleteColumn(int id);

    }
}
