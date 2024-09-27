using Sahibinden.Core.EntityFramework;
using Sahibinden.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.DataAccess.Abstract
{
    public interface IUserDal: IEntityRepository<User>
    {
    }
}
