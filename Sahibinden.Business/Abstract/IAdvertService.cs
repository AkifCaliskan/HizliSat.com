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

        IQueryable<Advert> GetQueryable(bool status);
        IQueryable<Advert> GetQueryable(Expression<Func<Advert, bool>> filter = null);
        IQueryable<Advert> GetQueryableSearch();
        List<Advert> GetAll();
        Advert Add(Advert advert);
        Advert Update(Advert advert);
        Advert GetById(int id);
        void UpdateDeleteColumn(int id);
        void DeleteColumn(Advert advert);
    }
}
