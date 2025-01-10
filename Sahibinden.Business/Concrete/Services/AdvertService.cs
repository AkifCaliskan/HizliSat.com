using AutoMapper;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Model.Advert;
using Sahibinden.Core.EntityFramework;
using Sahibinden.DataAccess;
using Sahibinden.DataAccess.Repositories;
using Sahibinden.DataAccess.UnitOfWork;
using Sahibinden.Entities.Concrete;
using Sahibinden.Model.Advert;
using Sahibinden.Model.AdvertDetail;
using Sahibinden.Model.Category;
using Sahibinden.Model.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Concrete.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdvertService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Advert> Add(AdvertAddModel advertAddModel)
        {
            var advert = _mapper.Map<Advert>(advertAddModel);
            await _unitOfWork.GetRepository<Advert>().AddAsync(advert);
            await _unitOfWork.SaveChangesAsync();
            return advert;
        }

        public async Task Delete(int id)
        {
            var repository = _unitOfWork.GetRepository<Advert>();
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Advert Not Deleted");
            }
            repository.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Advert> GetById(int id)
        {
            var repositories = _unitOfWork.GetRepository<Advert>();
            var entity = await repositories.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Hata");
            }
            return entity;
        }

        public async Task<IEnumerable<Advert>> List(AdvertListModel advertListModel)
        {
            var adverts = await _unitOfWork.GetRepository<Advert>().GetAllAsync();
            return adverts;
        }

        public async Task<Advert> Update(AdvertEditModel advertEditModel)
        {
            var repository = _unitOfWork.GetRepository<Advert>();
            var updatedItem = await repository.GetByIdAsync(advertEditModel.Id);
            if (updatedItem == null)
            {
                throw new Exception("Hata");
            }
            _mapper.Map<Advert>(advertEditModel);
            repository.UpdateAsync(updatedItem);
            await _unitOfWork.SaveChangesAsync();
            return updatedItem;

        }
    }
}
