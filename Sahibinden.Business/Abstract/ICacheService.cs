using Sahibinden.Business.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Abstract
{
    public interface ICacheService
    {
        T GetFromCache<T>(string key);
        void SetToCache<T>(string key, T value, TimeSpan expiration);
        void RemoveFromCache(string key);
        bool TryGetUser(int userId, out(UserDto User, List<int> Permissions)cachedUser);
    }
}
