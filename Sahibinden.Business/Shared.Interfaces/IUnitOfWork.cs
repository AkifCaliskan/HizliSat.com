using Sahibinden.DataAccess.Repositories;
using Sahibinden.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Shared.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Advert> Adverts { get; }
        IGenericRepository<AdvertDetail> AdvertDetails { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<CategoryFeature> categoryFeatures { get; }
        IGenericRepository<Image> Images { get; }
        IGenericRepository<User> Users { get; }
        Task<int> CommitAsync();
        Task ExecuteInTransactionAsync(Func<Task> action);
    }
}
