using Sahibinden.Business.Abstract;
using Sahibinden.Core.EntityFramework;
using Sahibinden.DataAccess.Abstract;
using Sahibinden.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Concrete.Services
{
    public class UserService : IUserService
    {
        private IUserDal _userDal;
        private IQueryableRepository<User> _queryableRepository;
        public UserService(IUserDal userDal, IQueryableRepository<User> queryableRepository)
        {
            _userDal = userDal;
            _queryableRepository = queryableRepository;
        }

        public User Add(User authorizedDomain)
        {
            return _userDal.Add(authorizedDomain);
        }

        public User GetById(int id)
        {
            return _userDal.Get(x=>x.Id == id);
          
        }

        public IQueryable<User> GetQueryable(bool status)
        {
            return _queryableRepository.Table.Where(x => x.Status == status);
        }

        public IQueryable<User> GetQueryable(Expression<Func<User, bool>> filter = null)
        {
            return filter == null ? _queryableRepository.Table : _queryableRepository.Table.Where(filter);
        }

        public IQueryable<User> GetQueryableSearch()
        {
            return _queryableRepository.Table;
        }

        public List<User> GetUsersAll()
        {
            return _userDal.GetList();
        }

        public User Update(User user)
        {
            return _userDal.Update(user);
        }

        public void UpdateDeleteColumn(int id)
        {
            var deletedItem = GetById(id);
            Update(deletedItem);

        }
    }
}
