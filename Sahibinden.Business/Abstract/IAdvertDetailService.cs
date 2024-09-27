using Sahibinden.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Abstract
{
    public interface IAdvertDetailService
    {
        List<AdvertDetail> GetAll();
        AdvertDetail Add(AdvertDetail advertdetail);
        AdvertDetail Update(AdvertDetail advertdetail);
        AdvertDetail GetById(int id);
        void UpdateDeleteColumn(int id);
        void DeleteColumn(AdvertDetail advertDetail);
        IQueryable<AdvertDetail> GetQueryable(bool status);
        IQueryable<AdvertDetail> GetQueryable(Expression<Func<AdvertDetail, bool>> filter = null);
        IQueryable<AdvertDetail> GetQueryableSearch();
    }
}
