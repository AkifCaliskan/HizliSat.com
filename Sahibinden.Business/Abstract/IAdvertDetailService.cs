using Sahibinden.Business.Model.AdvertDetail;
using Sahibinden.Entities.Concrete;
using Sahibinden.Model.AdvertDetail;
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
        Task<AdvertDetail> Add(AdvertDetailAdd advertDetailAdd);
        Task<List<AdvertDetail>> List(AdvertDetailListModel advertDetailListModel);
        Task<AdvertDetail> Update(AdvertDetailEditModel advertDetailEditModel);
        Task<AdvertDetail> GetById (int id);
        Task Delete(int id);

    }
}
