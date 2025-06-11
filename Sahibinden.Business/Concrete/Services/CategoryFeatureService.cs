using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Model.CategoryFeatures;
using Sahibinden.DataAccess.UnitOfWork;
using Sahibinden.Entities.Concrete;

namespace Sahibinden.Business.Concrete.Services
{
    public class CategoryFeatureService : ICategoryFeaturesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _categoryService;

        public CategoryFeatureService(IMapper mapper, IUnitOfWork unitOfWork, ICategoryService categoryService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
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
                ResultWrapperService<CategoryFeature>.FailureResult("Kategori Özellikleri bulunamadı");

            }
            repository.DeleteAsync(deletedItem);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<CategoryFeaturesListModel>> GetByCategoryIdAsync(int categoryId)
        {
            var rootId = await _categoryService.GetRootCategoryIdAsync(categoryId);

            var features = await _unitOfWork.GetRepository<CategoryFeature>()
                .Query()
                // YENİ EKLENEN KISIM: Her bir özellik için ilişkili Options'ları da veritabanından getir.
                .Include(cf => cf.Options)
                .Where(cf => cf.CategoryId == rootId)
                .ToListAsync();

            // Select ifadesi artık çok daha basit!
            return features.Select(cf => new CategoryFeaturesListModel
            {
                Id = cf.Id,
                Name = cf.Name,
                categoryId = cf.CategoryId,
                InputType = cf.InputType,

                // Options listesini veritabanından gelen veriye göre doldur.
                // cf.Options'ın null olmadığını ve eleman içerdiğini kontrol etmek her zaman iyidir.
                Options = (cf.Options != null && cf.Options.Any())
                    ? cf.Options.OrderBy(o => o.DisplayOrder).Select(o => o.OptionText).ToList()
                    : null
            }).ToList();
        }

        public Task<CategoryFeature> GetById(int id)
        {
            var repository = _unitOfWork.GetRepository<CategoryFeature>();
            var entity = repository.GetByIdAsync(id);
            return entity;
        }

        public async Task<List<CategoryFeaturesListModel>> List()
        {
            var repository = await _unitOfWork.GetRepository<CategoryFeature>().Query().Where(x => x.Id == x.CategoryId).ToListAsync();
            var categoryFeature = repository.Select(x => new CategoryFeaturesListModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            return categoryFeature;
        }

        public bool Update(CategoryFeature categoryFeature)
        {
            var repository = _unitOfWork.GetRepository<CategoryFeature>();
            repository.GetByIdAsync(categoryFeature.Id);
            if (categoryFeature.Id == null)
            {
                ResultWrapperService<CategoryFeature>.FailureResult("Kategori Özellikleri bulunamadı");
            }
            repository.UpdateAsync(categoryFeature);
            _unitOfWork.SaveChangesAsync();
            return true;

        }

    }


}
