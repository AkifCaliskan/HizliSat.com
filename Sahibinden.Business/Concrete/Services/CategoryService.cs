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
    public class CategoryService : ICategoryService
    {
        private ICategoryDal _categoryDal;
        private IQueryableRepository<Category> _queryableRepository;
        public CategoryService(ICategoryDal categoryDal, IQueryableRepository<Category> queryableRepository)
        {
            _categoryDal = categoryDal;
            _queryableRepository = queryableRepository;
        }

        public Category Add(Category authorizedDomain)
        {
            return _categoryDal.Add(authorizedDomain);
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetList();
        }

        public Category GetById(int id)
        {
            return _categoryDal.Get(x => x.Id == id);
        }

        public IQueryable<Category> GetQueryable(bool status)
        {
            return _queryableRepository.Table.Where(x => x.Status == status);
        }

        public IQueryable<Category> GetQueryable(Expression<Func<Category, bool>> filter = null)
        {
            return filter == null ? _queryableRepository.Table : _queryableRepository.Table.Where(filter);
        }

        public IQueryable<Category> GetQueryableSearch()
        {
            return _queryableRepository.Table;
        }

        public Category Update(Category category)
        {
            return _categoryDal.Update(category);
        }

        public void UpdateDeleteColumn(int id)
        {
            var deletedItem = GetById(id);
            Update(deletedItem);
        }
        public void DeleteColumn(Category category)
        {
            _categoryDal.Delete(category);
        }
    }
}
