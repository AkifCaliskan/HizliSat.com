using Sahibinden.Entities.Concrete;
using Sahibinden.Model.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Abstract
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> List(CategoryListModel categoryListModel);
        Task<Category> Add(CategoryAddModel categoryAddModel);
        Task<Category> Update(CategoryEditModel categoryEditModel);
        Task<Category> GetById(int id);

        Task Delete(int id);
    }
}
