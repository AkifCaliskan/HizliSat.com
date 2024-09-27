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
    public class ImageService : IImageService
    {
        private IImagesDal _imagesDal;
        private IQueryableRepository<Image> _queryableRepository;
        public ImageService(IImagesDal imagesDal , IQueryableRepository<Image> queryableRepository)
        {
            _imagesDal = imagesDal;
            _queryableRepository = queryableRepository;
        }

        public Image Add(Image authorizedDomain)
        {
            return _imagesDal.Add(authorizedDomain);
            
        }

        public void DeleteColumn(Image image)
        {
            _imagesDal.Delete(image);
        }

        public List<Image> GetAll()
        {
            return _imagesDal.GetList();
        }

        public Image GetById(int id)
        {
            return _imagesDal.Get(x=> x.Id == id);
        }

        public IQueryable<Image> GetQueryable(bool status)
        {
            return _queryableRepository.Table.Where(x => x.Status == status);
        }

        public IQueryable<Image> GetQueryable(Expression<Func<Image, bool>> filter = null)
        {
            return filter == null ? _queryableRepository.Table : _queryableRepository.Table.Where(filter);
        }

        public IQueryable<Image> GetQueryableSearch()
        {
            return _queryableRepository.Table;
        }

        public Image Update(Image image)
        {
            return _imagesDal.Update(image);
        }

        public void UpdateDeleteColumn(int id)
        {
            var deletedItem = GetById(id);
            Update(deletedItem);
        }
    }
}
