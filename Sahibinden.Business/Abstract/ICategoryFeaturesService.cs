using Sahibinden.Business.Model.CategoryFeatures;
using Sahibinden.Entities.Concrete;

namespace Sahibinden.Business.Abstract
{
    public interface ICategoryFeaturesService
    {

        Task<List<CategoryFeaturesListModel>> List();
        Task<List<CategoryFeaturesListModel>> GetByCategoryIdAsync(int categoryId);
        Task<CategoryFeature> Add(CategoryFeature categoryFeature);
        bool Update(CategoryFeature categoryFeature);
        Task<CategoryFeature> GetById(int id);
        void Delete(int id);

    }
}
