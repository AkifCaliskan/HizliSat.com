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
    public class CategoryFeatureService : ICategoryFeaturesService
    {
        private ICategoryFeaturesDal _categoryFeaturesDal;
        private IQueryableRepository<CategoryFeature> _queryableRepository;
        public CategoryFeatureService(ICategoryFeaturesDal categoryFeaturesDal, IQueryableRepository<CategoryFeature> queryableRepository)
        {
            _categoryFeaturesDal = categoryFeaturesDal;
            _queryableRepository = queryableRepository;
        }

        public CategoryFeature Add(CategoryFeature authorizedDomain)
        {
          return _categoryFeaturesDal.Add(authorizedDomain);
        }

        public void DeleteColumn(CategoryFeature categoryFeature)
        {
           _categoryFeaturesDal.Delete(categoryFeature);
        }

        public List<CategoryFeature> GetAll()
        {
            return _categoryFeaturesDal.GetList();
        }

        public CategoryFeature GetById(int id)
        {
           return _categoryFeaturesDal.Get(x=> x.Id == id);
        }

        public IQueryable<CategoryFeature> GetQueryable(bool status)
        {
            return _queryableRepository.Table.Where(x=>x.Status == status); 
        }

        public IQueryable<CategoryFeature> GetQueryable(Expression<Func<CategoryFeature, bool>> filter = null)
        {
            return filter == null ? _queryableRepository.Table : _queryableRepository.Table.Where(filter);
        }

        public IQueryable<CategoryFeature> GetQueryableSearch()
        {
            return _queryableRepository.Table;
        }

        public CategoryFeature Update(CategoryFeature categoryFeature)
        {
            return _categoryFeaturesDal.Update(categoryFeature);
        }

        public void UpdateDeleteColumn(int id)
        {
            var deletedItem = GetById(id);
            Update(deletedItem);
        }
    }
}
