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

        Task<IEnumerable<CategoryFeature>> List(CategoryFeature categoryFeature);
        Task<CategoryFeature> Add(CategoryFeature categoryFeature);
        bool Update(CategoryFeature categoryFeature);
        Task<CategoryFeature> GetById(int id);
        void Delete(int id);

    }
}
