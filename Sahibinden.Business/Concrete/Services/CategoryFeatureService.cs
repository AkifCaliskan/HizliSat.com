using AutoMapper;
using Sahibinden.Business.Abstract;
using Sahibinden.Core.EntityFramework;
using Sahibinden.DataAccess.UnitOfWork;
using Sahibinden.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Concrete.Services
{
    public class CategoryFeatureService : ICategoryFeaturesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryFeatureService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryFeature> Add(CategoryFeature categoryFeature)
        {
            var category = _mapper.Map<CategoryFeature>(categoryFeature);
            await _unitOfWork.GetRepository<CategoryFeature>().AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return category;
        }

        public async void Delete(int id)
        {
            var repository = _unitOfWork.GetRepository<CategoryFeature>();
            var deletedItem = await repository.GetByIdAsync(id);
            if (deletedItem == null)
            {
                throw new Exception("Hata");

            }
            repository.DeleteAsync(deletedItem);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task<CategoryFeature> GetById(int id)
        {
            var repository = _unitOfWork.GetRepository<CategoryFeature>();
            var entity = repository.GetByIdAsync(id);
            return entity;
        }

        public async Task<IEnumerable<CategoryFeature>> List(CategoryFeature categoryFeature)
        {
            var repository = _unitOfWork.GetRepository<CategoryFeature>();
            return await repository.GetAllAsync();
        }

        public bool Update(CategoryFeature categoryFeature)
        {
            var repository = _unitOfWork.GetRepository<CategoryFeature>();
            repository.GetByIdAsync(categoryFeature.Id);
            if (categoryFeature.Id == null)
            {
                throw new Exception("Hata");
            }
            repository.UpdateAsync(categoryFeature);
            _unitOfWork.SaveChangesAsync();
            return true;

        }

    }


}
