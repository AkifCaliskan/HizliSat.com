using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Concrete.Services;
using Sahibinden.Core.EntityFramework;
using Sahibinden.Entities.Concrete;
using Sahibinden.Model.AdvertDetail;
using System.Xml;

namespace Sahibinden.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertDetailController : ControllerBase
    {
        private IAdvertDetailService _advertDetailService;
        private IQueryableRepository<AdvertDetail> _queryableRepository;
        private IImageService _imageService;
        private IHttpContextAccessor _contextAccessor;
        public AdvertDetailController(IAdvertDetailService advertDetailService, IImageService imageService, IHttpContextAccessor contextAccessor)
        {
            _advertDetailService = advertDetailService;
            _imageService = imageService;
            _contextAccessor = contextAccessor;
        }
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {

            var advertdetail = _advertDetailService.GetQueryable(true).Include(p => p.Advert).FirstOrDefault(p => p.Advert.Name == name);
            if (advertdetail == null)
            {
                return NotFound("Aradığınız Kayıt Bulunamadı.");
            }
            var images = _imageService.GetQueryable(true).Where(x => x.AdvertId == advertdetail.AdvertId).Select(p => p.Images).ToList();
            var model = new AdvertDetailListModel()
            {
                Id = advertdetail.Id,
                Images = ImageUrl(images),
                AdvertId = advertdetail.AdvertId,
                RecordDate = advertdetail.RecordDate.ToString("dd/MM/yyyy HH:mm"),
                Status = advertdetail.Status,
            };
            return Ok(model);


        }
        [HttpGet]
        public IActionResult GetAll()
        {
            IQueryable<AdvertDetail> query = _advertDetailService.GetQueryable(true);
            var model = query.Select(p => new AdvertDetailListModel()
            {
                Id = p.Id,
            });
            return Ok(model);

        }

        [HttpPost]
        public IActionResult Add([FromBody] AdvertDetailAdd advertDetail)
        {
            if (!ModelState.IsValid)
            {
                return NotFound("Eksik Veya Hatalı Girdiniz.");
            }

            var addAdvertDetail = new AdvertDetail
            {
                AdvertId = advertDetail.AdvertId,
                RecordDate = DateTime.Now,
                Status = true,
            };


            if (advertDetail.CategoryId == 19)
            {
            }
            else if (advertDetail.CategoryId == 1)
            {

            }



            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var deletedItem = _advertDetailService.GetById(id);
            if (deletedItem == null)
            {
                return NotFound("Silme İşlemi Başarısız");
            }
            _advertDetailService.DeleteColumn(deletedItem);
            return Ok("Silme İşlemi Başarılı.");
        }
        [HttpPut]
        public IActionResult Update([FromBody] AdvertDetailEditModel model, int id)
        {
            var updatedItem = _advertDetailService.GetById(id);
            if (updatedItem == null)
            {
                return NotFound("Güncelleme İşlemi Başarısız.");
            }

            updatedItem.AdvertId = model.AdvertId;
            updatedItem.Status = model.Status;

            _advertDetailService.Update(updatedItem);

            return Ok("Güncelleme İşlemi Başarılı");

        }

        private List<string> ImageUrl(List<string> imageUrls)
        {
            var result = new List<string>();
            var request = _contextAccessor.HttpContext.Request;

            if (imageUrls != null)
            {
                foreach (var imageUrl in imageUrls)
                {
                    var splitUrl = imageUrl.Split("C:/Users/25ahm/OneDrive/Masaüstü/Resimler/");
                    if (splitUrl.Length > 1)
                    {
                        var url = $"{request.Scheme}://{request.Host}/{splitUrl[1]}";
                        result.Add(url);
                    }
                }
            }

            return result;
        }
    }
}