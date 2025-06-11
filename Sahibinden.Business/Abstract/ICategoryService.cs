using Sahibinden.Business.Model.Category;
using Sahibinden.Entities.Concrete;

namespace Sahibinden.Business.Abstract
{
    public interface ICategoryService
    {
        Task<List<CategoryListModel>> GetSubCategories(int parentId);
        Task<int> GetRootCategoryIdAsync(int categoryId);
        Task<List<CategoryListModel>> List();
        Task<Category> Add(CategoryAddModel categoryAddModel);
        Task<Category> Update(CategoryEditModel categoryEditModel);
        Task<Category> GetById(int id);
        Task Delete(int id);
    }
}
