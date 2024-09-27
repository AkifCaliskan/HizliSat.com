using Sahibinden.Entities.Concrete;
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
        List<Category> GetAll();
        Category Add(Category category);
        Category Update(Category category);
        Category GetById(int id);
        public void UpdateDeleteColumn(int id);
        IQueryable<Category> GetQueryable(bool status);
        IQueryable<Category> GetQueryable(Expression<Func<Category, bool>> filter = null);
        IQueryable<Category> GetQueryableSearch();
        void DeleteColumn(Category category);
    }
}
