using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdvertService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Advert> Add(AdvertAddModel advertAddModel, IFormFileCollection files)
        {
            if (files == null || files.Count == 0)
            {
                throw new Exception("Lütfen en az bir fotoğraf yükleyiniz.");
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var advert = _mapper.Map<Advert>(advertAddModel);
                advert.Images = new List<Image>(); // Liste başlatılıyor

                await _unitOfWork.GetRepository<Advert>().AddAsync(advert);
                await _unitOfWork.SaveChangesAsync(); // İlan ID'sinin oluşması için kaydet

                // İlan özelliklerini kaydet
                foreach (var feature in advertAddModel.FeatureValues)
                {
                    var detail = new AdvertDetail
                    {
                        AdvertId = advert.Id,
                        CategoryFeatureId = feature.Key,
                        Value = feature.Value
                    };
                    await _unitOfWork.GetRepository<AdvertDetail>().AddAsync(detail);
                }

                // Fotoğrafları kaydet
                bool isFirstImage = true;
                foreach (var file in files)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ilan-resimleri");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    await using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    var image = new Image
                    {
                        AdvertId = advert.Id,
                        ImageUrl = "/ilan-resimleri/" + uniqueFileName,
                        IsCover = isFirstImage,
                        Status = true
                    };
                    await _unitOfWork.GetRepository<Image>().AddAsync(image);

                    isFirstImage = false;
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return advert;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
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
            var query = await _unitOfWork.GetRepository<Advert>().Query()
                .Include(p => p.Category)
                .Include(p => p.User)
                .Include(p => p.Images)
                .ToListAsync();

            var result = query.Select(a => new AdvertListModel
            {
                Id = a.Id,
                Name = a.Name,
                Status = a.Status,
                Description = a.Description,
                CategoryName = a.Category.Name,
                RecordDate = a.RecordDate.ToString("dd MMM yyyy", new CultureInfo("tr-TR")),
                FirstName = $"{a.User.FirstName} {a.User.LastName}",

                CoverImageUrl = a.Images.FirstOrDefault(p => p.IsCover)?.ImageUrl
                              ?? a.Images.FirstOrDefault()?.ImageUrl
                              ?? "/images/no-image.png"
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
