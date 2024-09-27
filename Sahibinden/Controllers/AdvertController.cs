using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Concrete.Services;
using Sahibinden.Core.EntityFramework;
using Sahibinden.Entities.Concrete;
using Sahibinden.Model.Advert;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;

namespace Sahibinden.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private IAdvertService _advertServce;
        private IAdvertDetailService _advertDetailServce;
        private IImageService _imageServce;
        private IConfiguration _configuration;
        private IHttpContextAccessor _contextAccessor;
        public AdvertController(IAdvertService advertService, IAdvertDetailService advertDetailServce, IImageService imageServce, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _advertServce = advertService;
            _advertDetailServce = advertDetailServce;
            _imageServce = imageServce;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var advert = _advertServce.GetQueryable(true).Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            if (advert == null)
            {
                return NotFound("Aradığınz Kayıt Bulunamadı");
            }
            var model = new AdvertEditModel()
            {
                Name = advert.Name,
                Description = advert.Description,
                CategoryId = advert.Category.Id,
                CategoryName = advert.Category.Name,

            };
            return Ok(model);
        }

        [HttpGet]
        [Route("Name/{name}")]
        public IActionResult GetByName(string name)
        {
            var advert = _advertServce.GetQueryable(true).Include(p => p.Category).FirstOrDefault(p => p.Name == name);
            if (advert == null)
            {
                return NotFound("Aradığınz Kayıt Bulunamadı");
            }

            var advertDetails = _advertDetailServce.GetQueryable(true).Include(p => p.Advert).Where(p => p.AdvertId == advert.Id).ToList();
            var advertImages = _imageServce.GetQueryable(true).Where(p => p.AdvertId == advert.Id).ToList();

            var model = new AdvertEditModel()
            {
                Name = advert.Name,
                Description = advert.Description,
                CategoryId = advert.CategoryId,
                CategoryName = advert.Category.Name,
                AdvertDetails = advertDetails.Select(p => new AdvertDetails()
                {
                    Id = p.Id,
                   

                }).ToList(),
                AdvertImages = advertImages.Select(i => new AdvertImage()
                {
                    Id = i.Id,
                    Url = ImageUrls(i.Images)
                }).ToList(),
            };
            return Ok(model);
        }

        [HttpPut]
        [Route("UpdateByName/{name}")]
        public IActionResult UpdateByName(string name, [FromBody] AdvertEditModel updatedModel)
        {
            // İlanı getir
            var advert = _advertServce.GetQueryable(true)
                                      .Include(p => p.Category)
                                      .FirstOrDefault(p => p.Name == name);
            if (advert == null)
            {
                return NotFound("Güncellenmek istenen ilan bulunamadı.");
            }

            // İlan bilgilerini güncelle
            advert.Name = updatedModel.Name;
            advert.Description = updatedModel.Description;
            advert.CategoryId = updatedModel.CategoryId;
            _advertServce.Update(advert);

            // İlan detaylarını güncelle
            var advertDetails = _advertDetailServce.GetQueryable(true)
                                                   .Where(p => p.AdvertId == advert.Id)
                                                   .ToList();
            foreach (var detail in advertDetails)
            {
                var updatedDetail = updatedModel.AdvertDetails?.FirstOrDefault(p => p.Id == detail.Id);
                if (updatedDetail != null)
                {

                    _advertDetailServce.Update(detail);
                }
            }

            var existingImages = _imageServce.GetQueryable(true)
                                             .Where(img => img.AdvertId == advert.Id)
                                             .ToList();


            var imagesToKeep = updatedModel.AdvertImages
                                            .Where(img => existingImages.Any(ei => ei.Id == img.Id))
                                            .ToList();


            var imagesToDelete = existingImages
                                 .Where(ei => updatedModel.AdvertImages.Any(img => img.Id == ei.Id))
                                 .ToList();


            foreach (var imageToDelete in imagesToDelete)
            {
                _imageServce.DeleteColumn(imageToDelete);
            }

            // Yeni resimleri ekle
            foreach (var newImage in updatedModel.AdvertImages)
            {
                var existingImage = existingImages.FirstOrDefault(img => img.Id == newImage.Id);

                if (existingImage == null)
                {
                    var repo = _configuration.GetValue<string>("RepoPath");
                    var fileName = Path.GetFileName(newImage.Url);
                    // URL'yi oluştur
                    var imageUrl = $"{Request.Scheme}://{Request.Host}/{repo}/{fileName}";

                    var image = new Image
                    {
                        AdvertId = advert.Id,
                        Images = imageUrl,
                        RecordDate = DateTime.Now,
                        Status = true,
                    };
                    _imageServce.Add(image);
                }
            }

            return Ok("İlan başarıyla güncellendi.");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IQueryable<Advert> query = _advertServce.GetQueryable(true).Include(p => p.Category);
            var model = query.Select(p => new AdvertListModel()
            {
                CategoryName = p.Category.Name,
                Description = p.Description,
                Id = p.Id,
                Name = p.Name,
                RecordDate = p.RecordDate.ToString("dd/MM/yyyy"),
                FirstImage = ImageUrl(_imageServce.GetQueryable(true).FirstOrDefault(x => x.AdvertId == p.Id) != null ? _imageServce.GetQueryable(true).FirstOrDefault(x => x.AdvertId == p.Id).Images : "", _contextAccessor.HttpContext.Request),
                UserId = p.UserId,
            });
            return Ok(model);
        }

        [Authorize]
        [HttpGet]
        [Route("Myadvert")]
        public IActionResult GetAdvertUserId()
        {
            var user = _contextAccessor.HttpContext?.User;

            var userInformation = user.FindFirst(ClaimTypes.Actor)?.Value;
            if (userInformation == null)
            {
                return Unauthorized();
            }
            var userId = Convert.ToInt32(userInformation);
            IQueryable<Advert> query = _advertServce.GetQueryable(true).Include(p => p.Category).Where(p => p.UserId == userId);
            var model = query.Select(p => new AdvertListModel()
            {
                UserId = p.UserId,
                Name = p.Name,
                Description = p.Description,
                CategoryName = p.Category.Name,
                Id = p.Id,
                RecordDate = p.RecordDate.ToString("dd/MM/yyyy hh:mm"),
                FirstImage = ImageUrl(_imageServce.GetQueryable(true).FirstOrDefault(x => x.AdvertId == p.Id) != null ? _imageServce.GetQueryable(true).FirstOrDefault(x => x.AdvertId == p.Id).Images : "", _contextAccessor.HttpContext.Request),
            });

            return Ok(model);

        }
        [Authorize]
        [HttpPost]
        public IActionResult Add([FromBody] AdvertAddModel advert)
        {
            if (!ModelState.IsValid)
            {
                return NotFound("Eksik Veya Hatalı Girdiniz");
            }

            var user = _contextAccessor.HttpContext?.User;

            var userInformation = user.FindFirst(ClaimTypes.Actor)?.Value;
            if (userInformation == null)
            {
                return Unauthorized();
            }

            var addAdvert = _advertServce.Add(new Advert()
            {
                Name = !string.IsNullOrWhiteSpace(advert.Name) ? advert.Name : "",
                Description = !string.IsNullOrWhiteSpace(advert.Description) ? advert.Description : "",
                Status = advert.Status,
                RecordDate = DateTime.Now,
                CategoryId = advert.CategoryId,
                UserId = Convert.ToInt32(userInformation)
            });

            var advertDetail = _advertDetailServce.Add(new AdvertDetail()
            {
                AdvertId = addAdvert.Id,
                RecordDate = DateTime.Now,
                Status = advert.Status,

            });

            if (advert.AdvertImage.Length != 0)
            {
                var repo = _configuration.GetValue<string>("RepoPath");
                foreach (var advertImage in advert.AdvertImage)
                {
                    var images = _imageServce.Add(new Image()
                    {
                        AdvertId = addAdvert.Id,
                        Images = $"{repo}/{advertImage}",
                        RecordDate = DateTime.Now,
                        Status = true,
                    });
                }
            }


            return Ok("Başarılı");
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)

        {
            var deletedAdvert = _advertServce.GetById(id);
            if (deletedAdvert == null)
            {
                return NotFound("Silme İşlemi Başarısız");

            };

            _advertServce.DeleteColumn(deletedAdvert);
            return Ok("Silme İşlemi Başarılı");
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromBody] AdvertEditModel model, int id)
        {
            var updatedItem = _advertServce.GetById(id);

            if (updatedItem == null)
            {
                return NotFound("Güncelleme İşlemi Başarısız.");
            }

            updatedItem.Name = !string.IsNullOrWhiteSpace(model.Name) ? model.Name : "";
            updatedItem.Description = !string.IsNullOrWhiteSpace(model.Description) ? model.Description : "";
            updatedItem.CategoryId = model.CategoryId;

            _advertServce.Update(updatedItem);

            return Ok("Güncelleme İşlemi Başarılı");
        }

        public static string ImageUrl(string imageUrl, HttpRequest request)
        {
            var result = "";
            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                var splitUrl = imageUrl.Split("C:/Users/25ahm/OneDrive/Masaüstü/Resimler/");
                result = $"{request.Scheme}://{request.Host}/{splitUrl[1]}";
            }
            return result;
        }

        private string ImageUrls(string imageUrl)
        {
            var request = _contextAccessor.HttpContext?.Request;
            var result = "";
            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                var splitUrl = imageUrl.Split("C:/Users/25ahm/OneDrive/Masaüstü/Resimler/");
                result = $"{request.Scheme}://{request.Host}/{splitUrl[1]}";
            }
            return result;
        }

        private void DecodeToken(string token)
        {

        }
    }
}
