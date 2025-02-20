using AutoMapper;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Model.AdvertDetail;
using Sahibinden.Core.EntityFramework;
using Sahibinden.DataAccess.UnitOfWork;
using Sahibinden.Entities.Concrete;
using Sahibinden.Model.AdvertDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Concrete.Services
{
    public class AdvertDetailService : IAdvertDetailService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AdvertDetailService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AdvertDetail> Add(AdvertDetailAdd advertDetailAdd)
        {
            var advert = _mapper.Map<AdvertDetail>(advertDetailAdd);
            await _unitOfWork.GetRepository<AdvertDetail>().AddAsync(advert);
            await _unitOfWork.SaveChangesAsync();
            return advert;
        }

        public async Task Delete(int id)
        {
            var repository = _unitOfWork.GetRepository<AdvertDetail>();
            var deletedItem = await repository.GetByIdAsync(id);
            if (deletedItem == null)
            {
                ResultWrapperService<AdvertDetail>.FailureResult("İlan Detayları bulunamadı.");
            }
            repository.DeleteAsync(deletedItem);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<AdvertDetail> GetById(int id)
        {
            var repository = _unitOfWork.GetRepository<AdvertDetail>();
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
            {
                ResultWrapperService<AdvertDetail>.FailureResult("İlan Detayları bulunamadı");
            }
            return entity;
        }

        public async Task<IEnumerable<AdvertDetail>> List(AdvertDetailListModel advertDetailListModel)
        {
            var advertdetails = await _unitOfWork.GetRepository<AdvertDetail>().GetAllAsync();
            return advertdetails;
        }

        public async Task<AdvertDetail> Update(AdvertDetailEditModel advertDetailEditModel)
        {
            var repository = _unitOfWork.GetRepository<AdvertDetail>();
            var updatedItem = await repository.GetByIdAsync(advertDetailEditModel.AdvertId);
            if (updatedItem == null)
            {
                ResultWrapperService<AdvertDetail>.FailureResult("İlan Detayları bulunamadı");
            }
            repository.UpdateAsync(updatedItem);
            await _unitOfWork.SaveChangesAsync();
            return updatedItem;
        }
    }
}
