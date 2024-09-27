using Sahibinden.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Abstract
{
    public interface ICategoryFeaturesService
    {

        List<CategoryFeature> GetAll();
        CategoryFeature Add(CategoryFeature categoryFeature);
        CategoryFeature Update(CategoryFeature categoryFeature);
        CategoryFeature GetById(int id);
        void UpdateDeleteColumn(int id);
        void DeleteColumn(CategoryFeature categoryFeature);
        IQueryable<CategoryFeature> GetQueryable(bool status);
        IQueryable<CategoryFeature> GetQueryable(Expression<Func<CategoryFeature, bool>> filter = null);
        IQueryable<CategoryFeature> GetQueryableSearch();
    }
}
