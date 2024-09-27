using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Concrete.Services;
using Sahibinden.Entities.Concrete;
using Sahibinden.Model.Category;
using System.Reflection.Metadata.Ecma335;


namespace Sahibinden.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        private IImageService _imageService;
        private IAdvertService _advertService;
        private IHttpContextAccessor _contextAccessor;
        public CategoryController(ICategoryService categoryService, IImageService imageService, IAdvertService advertService, IHttpContextAccessor contextAccessor)
        {
            _categoryService = categoryService;
            _imageService = imageService;
            _advertService = advertService;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public IActionResult GetCategory()
        {
            IQueryable<Category> query = _categoryService.GetQueryable(true);
            var model = query.Select(x => new CategoryListModel
            {
                Description = x.Description,
                Id = x.Id,
                Name = x.Name,
                RecordDate = x.RecordDate.ToString("dd,MMM,yyyy"),
            });
            return Ok(model);
        }


        [HttpGet]
        [Route("id")]
        public IActionResult GetById(int id)
        {
            var category = _categoryService.GetQueryable(true).Include(p => p.Adverts).FirstOrDefault(p => p.Id == id);
            if (category == null)
            {
                return NotFound("Aradığınız Kayıt Bulunumadı");

            }

            var advertId = category.Adverts.Select(p => p.Id);
            var model = new CategoryDetailModel
            {
                Id = id,
                Name = !string.IsNullOrWhiteSpace(category.Name) ? category.Name : "",
                RecordDate = category.RecordDate.ToString("dd,MMM,yyyy"),
                Adverts = category.Adverts.Select(x => new CategoryByAdvert()
                {
                    id = x.Id,
                    Name = x.Name,
                    Image = ImageUrl(_imageService.GetQueryable(true).FirstOrDefault(c => advertId.Contains(c.AdvertId))?.Images ?? "")
                }).ToList(),

            };
            return Ok(model);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CategoryAddModel category)
        {
            if (!ModelState.IsValid)
            {
                return NotFound("Eksik Veya Hatalı Girdiniz.");
            }
            var addCategory = _categoryService.Add(new Category()
            {
                Name = category.Name,
                RecordDate = DateTime.Now,
                Description = category.Description,
                ParentId = category.ParentId,
                Status = category.CategoryStatus,
            }); return Ok(addCategory);
        }
        [HttpGet]
        [Route("CategoryGetName/{name}")]
        public IActionResult CategoryGetName(string name)
        {
            var category = _categoryService.GetQueryable(true).Include(p => p.Adverts).FirstOrDefault(p => p.Name == name);
            if (category == null)
            {
                return NotFound("Aradığınız Kayıt Bulunumadı");

            }
            var adverts = _advertService.GetQueryable(true).Where(p => p.CategoryId == category.Id).ToList();
            var model = new CategoryDetailModel
            {
                Id = category.Id,
                Name = !string.IsNullOrWhiteSpace(category.Name) ? category.Name : "",
                RecordDate = category.RecordDate.ToString("dd,MMM,yyyy"),
                Adverts = adverts.Select(x => new CategoryByAdvert()
                {
                    id = x.Id,
                    Name = x.Name,
                    Image = ImageUrl(_imageService.GetQueryable(true).FirstOrDefault(c => c.AdvertId == x.Id)?.Images ?? "")
                }).ToList(),

            };
            return Ok(model);
        }

        [HttpDelete("Protected")]
        public IActionResult Delete(int id)
        {
            var deleteCategory = _categoryService.GetById(id);
            if (deleteCategory == null)
            {
                return NotFound("Silme İşlemi Başarısız.");
            }
            _categoryService.DeleteColumn(deleteCategory);
            return Ok("Silme İşlemi Başarılı");
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] CategoryEditModel model, int id)
        {
            var updatedCategory = _categoryService.GetById(id);
            if (updatedCategory == null)
            {
                return NotFound("Güncelleme İşlemi Başarısız");
            }
            updatedCategory.Name = !string.IsNullOrWhiteSpace(model.Name) ? model.Name : "";
            updatedCategory.Status = model.Status;
            updatedCategory.Description = model.Description;
            updatedCategory.ParentId = model.ParentId;
            _categoryService.Update(updatedCategory);
            return Ok("Güncelleme İşlemi Başarılı");
        }

        private string ImageUrl(string imageUrl)
        {
            var result = "";
            var request = _contextAccessor.HttpContext.Request;
            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                var splitUrl = imageUrl.Split("C:/Users/25ahm/OneDrive/Masaüstü/Resimler/");
                result = $"{request.Scheme}://{request.Host}/{splitUrl[1]}";
            }
            return result;
        }
    }
}

