using Sahibinden.Business.Model.User;
using Sahibinden.Entities.Concrete;
using Sahibinden.Model.Category;
using Sahibinden.Model.User;
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

        Task<IEnumerable<User>> List();
        Task<User> GetById(int id);
        Task Delete(int id);
    }
}
