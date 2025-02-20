using Sahibinden.Business.DTO_s;
using Sahibinden.Business.Model.User;
using Sahibinden.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Abstract
{
    public interface IAuthService
    {
        Task<bool> Register(UserRegisterModel userRegisterModel);
        Task<UserDto?> Authenticate(UserLoginDetailModel userLoginDetailModel);
    }
}
