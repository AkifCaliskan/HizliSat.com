using Sahibinden.Business.Abstract;
using Sahibinden.Core.EntityFramework;
using Sahibinden.DataAccess.Abstract;
using Sahibinden.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Concrete.Services
{
    public class AdvertDetailService : IAdvertDetailService
    {
        private IAdvertDetailDal _advertDetail;
        private IQueryableRepository<AdvertDetail> _queryableRepository;
        public AdvertDetailService(IAdvertDetailDal advertDetailDal, IQueryableRepository<AdvertDetail> queryableRepository)
        {
            _advertDetail = advertDetailDal;
            _queryableRepository = queryableRepository;
        }
        public AdvertDetail Add(AdvertDetail authorizedDomain)
        {
            return _advertDetail.Add(authorizedDomain);
        }

        public void DeleteColumn(AdvertDetail advertDetail)
        {
            _advertDetail.Delete(advertDetail);
        }

        public List<AdvertDetail> GetAll()
        {
            return _advertDetail.GetList();
        }

        public AdvertDetail GetById(int id)
        {
            return _advertDetail.Get(x => x.Id == id);
        }

        public IQueryable<AdvertDetail> GetQueryable(bool status)
        {
            return _queryableRepository.Table.Where(x => x.Status == status);
        }

        public IQueryable<AdvertDetail> GetQueryable(Expression<Func<AdvertDetail, bool>> filter = null)
        {
            return filter == null ? _queryableRepository.Table : _queryableRepository.Table.Where(filter);
        }

        public IQueryable<AdvertDetail> GetQueryableSearch()
        {
            return _queryableRepository.Table;
        }

        public AdvertDetail Update(AdvertDetail advertdetail)
        {
            return _advertDetail.Update(advertdetail);
        }

        public void UpdateDeleteColumn(int id)
        {
            var deletedItem = GetById(id);
            Update(deletedItem);
        }
    }
}
