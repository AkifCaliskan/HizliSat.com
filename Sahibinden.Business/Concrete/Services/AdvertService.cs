using System.Globalization;
using System.Text.Json.Nodes;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Model.Advert;
using Sahibinden.DataAccess.UnitOfWork;
using Sahibinden.Entities.Concrete;

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
                ResultWrapperService<Advert>.FailureResult("İlan bulunamadı");
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
                ResultWrapperService<Advert>.FailureResult("İlan bulunamadı");
            }
            return entity;
        }
        public async Task<List<AdvertListModel>> List()
        {
            var query = await _unitOfWork.GetRepository<Advert>().Query().Include(p => p.Category).Include(p => p.User).Include(p => p.Images).ToListAsync();

            var result = query.Select(a => new AdvertListModel
            {
                CategoryName = a.Category.Name,
                Description = a.Description,
                FirstImage = a.Images == null ? "" : a.Images.Images,
                Name = a.Name,
                Status = a.Status,
                Id = a.Id,
                RecordDate = a.RecordDate.ToString("dd MMM yyyy (hh:mm)", new CultureInfo("tr_TR")),
                FirstName =  $"{a.User.FirstName} {a.User.LastName}",
            }).ToList();

            return result;
        }

        public async Task<Advert> Update(AdvertEditModel advertEditModel)
        {
            var repository = _unitOfWork.GetRepository<Advert>();
            var updatedItem = await repository.GetByIdAsync(advertEditModel.Id);

            if (updatedItem == null)
            {
                ResultWrapperService<Advert>.FailureResult("İlan bulunamadı");
            }
            _mapper.Map(advertEditModel, updatedItem);
            repository.UpdateAsync(updatedItem);
            await _unitOfWork.SaveChangesAsync();
            return updatedItem;
        }
    }
}
