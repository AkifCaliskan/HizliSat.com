using System.Text.Json.Nodes;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Model.Category;
using Sahibinden.DataAccess.UnitOfWork;
using Sahibinden.Entities.Concrete;

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
                ResultWrapperService<Category>.FailureResult("Kategori Bulunamadı");
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
                ResultWrapperService<Category>.FailureResult("Kategori Bulunamadı");
            }
            return entity;
        }

        public async Task<List<CategoryListModel>> GetSubCategories(int parentId)
        {
            var query = await _unitOfWork.GetRepository<Category>().Query().Where(x => x.ParentId == parentId).ToListAsync();
            var categoryList = query.Select(a => new CategoryListModel
            {
                Description = a.Description,
                Name = a.Name,
                ParentId = a.ParentId,
                Id = a.Id,
                
            }).ToList();
            return categoryList;
        }

        public async Task<List<CategoryListModel>> List()
        {
            var query = await _unitOfWork.GetRepository<Category>().Query().ToListAsync();
            var categoryList = query.Select(a => new CategoryListModel
            {
                Description = a.Description,
                Name = a.Name,
                Id = a.Id,
                ParentId = a.ParentId == 0 ? null : a.ParentId,
                hasChild = query.Any(p => p.ParentId == a.Id),
            }).ToList();

            return categoryList;
        }

        public async Task<Category> Update(CategoryEditModel categoryEditModel)
        {
            var repository = _unitOfWork.GetRepository<Category>();
            var updatedItem = await repository.GetByIdAsync(categoryEditModel.Id);
            if (updatedItem == null)
            {
                ResultWrapperService<Category>.FailureResult("Kategori Bulunamadı");
            }
            repository.UpdateAsync(updatedItem);
            return updatedItem;
        }
    }
}


