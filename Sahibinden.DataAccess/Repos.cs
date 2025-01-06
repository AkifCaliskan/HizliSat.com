using Sahibinden.Core.EntityFramework;
using Sahibinden.DataAccess.Concrete;
using Sahibinden.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.DataAccess;

#region interfaces
public interface IAdvertDal : IEntityRepository<Advert>;
public interface ICategoryDal : IEntityRepository<Category>;

#endregion

#region classes
public class EfAdvertDal : EfEntityRepositoryBase<Advert, Context>, IAdvertDal;
public class EfCategoryDal : EfEntityRepositoryBase<Category, Context>, ICategoryDal;

#endregion
