using AutoMapper;
using Sahibinden.Business.Abstract;
using Sahibinden.Core.EntityFramework;
using Sahibinden.DataAccess.UnitOfWork;
using Sahibinden.Entities.Concrete;
using Sahibinden.Model.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Concrete.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Category> Add(CategoryAddModel categoryAddModel)
        {
            var category = _mapper.Map<Category>(categoryAddModel);
            await _unitOfWork.GetRepository<Category>().AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return category;
        }

        public async Task Delete(int id)
        {
            var repository = _unitOfWork.GetRepository<Category>();
            var deletedItem = await repository.GetByIdAsync(id);
            if (deletedItem == null)
            {
                throw new Exception("Hata");
            }
            repository.DeleteAsync(deletedItem);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Category> GetById(int id)
        {
            var repository = _unitOfWork.GetRepository<Category>();
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Hata");
            }
            return entity;
        }

        public async Task<IEnumerable<Category>> List(CategoryListModel categoryListModel)
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync();
            return categories;
        }

        public async Task<Category> Update(CategoryEditModel categoryEditModel)
        {
            var repository = _unitOfWork.GetRepository<Category>();
            var updatedItem =await repository.GetByIdAsync(categoryEditModel.Id);
            if (updatedItem == null)
            {
                throw new Exception("Hata");
            }
           repository.UpdateAsync(updatedItem);
            return updatedItem;
        }
    }
}


