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
    public class AdvertService : IAdvertService
    {
        private IAdvertDal _advertDal;
        private IQueryableRepository<Advert> _queryableRepository;
        public AdvertService(IAdvertDal advertDal, IQueryableRepository<Advert> queryableRepository)
        {
            _advertDal = advertDal;
            _queryableRepository = queryableRepository;
        }

        public Advert Add(Advert authorizedDomain)
        {
            return _advertDal.Add(authorizedDomain);
        }

        public List<Advert> GetAll()
        {
            return _advertDal.GetList();
        }

        public Advert GetById(int id)
        {
            return _advertDal.Get(x => x.Id == id);
        }

        public IQueryable<Advert> GetQueryable(bool status)
        {
            return _queryableRepository.Table.Where(x => x.Status == status);
        }

        public IQueryable<Advert> GetQueryable(Expression<Func<Advert, bool>> filter = null)
        {
            return filter == null ? _queryableRepository.Table : _queryableRepository.Table.Where(filter);
        }

        public IQueryable<Advert> GetQueryableSearch()
        {
            return _queryableRepository.Table;
        }

        public Advert Update(Advert advert)
        {
            return _advertDal.Update(advert);
        }

        public void UpdateDeleteColumn(int id)
        {
            var deletedItem = GetById(id);
            Update(deletedItem);
        }

        public void DeleteColumn(Advert advert)
        {
            _advertDal.Delete(advert);
        }

    }
}
