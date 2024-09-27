using Sahibinden.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Abstract
{
    public interface IImageService
    {
        IQueryable<Image> GetQueryable(bool status);
        IQueryable<Image> GetQueryable(Expression<Func<Image, bool>> filter = null);
        IQueryable<Image> GetQueryableSearch();
        List<Image> GetAll();
        Image Add(Image image);
        Image Update(Image image);
        Image GetById(int id);
        void UpdateDeleteColumn(int id);
        void DeleteColumn(Image image);
    }
}
