using Sahibinden.Business.Model.Advert;
using Sahibinden.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Abstract
{
    public interface IAdvertService
    {
        Task<Advert> Add(AdvertAddModel advertAddModel);
        Task<Advert> Update(AdvertEditModel advertEditModel);
        Task<Advert> GetById(int id);
        Task Delete(int id);
        Task<IEnumerable<Advert>> List(AdvertListModel advertListModel);
    }
}
