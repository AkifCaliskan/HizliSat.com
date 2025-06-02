using Sahibinden.Business.Model.User;
using Sahibinden.Entities.Concrete;

namespace Sahibinden.Business.Abstract
{
    public interface IUserService
    {

        Task<List<User>> List();
        Task<User> GetById(int id);
        Task Delete(int id);
        Task<User> Add(UserRegisterModel model);
    }
}
