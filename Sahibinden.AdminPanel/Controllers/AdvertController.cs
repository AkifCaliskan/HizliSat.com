using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Model.Advert;
using Syncfusion.EJ2.Base;

namespace Sahibinden.AdminPanel.Controllers
{
    [Route("Advert")]
    public class AdvertController : Controller
    {
        private readonly IAdvertService _advertService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICategoryService _categoryService;
        private readonly ICategoryFeaturesService _categoryFeaturesService;

        public AdvertController(IAdvertService advertService, IHttpClientFactory httpClientFactory, ICategoryService categoryService, ICategoryFeaturesService categoryFeaturesService)
        {
            _advertService = advertService;
            _httpClientFactory = httpClientFactory;
            _categoryService = categoryService;
            _categoryFeaturesService = categoryFeaturesService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7001/api/Advert/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return NotFound("İlan silinemedi.");
        }
        [HttpPost("GetList")]
        public async Task<IActionResult> GetAdverts([FromBody] DataManagerRequest dm)
        {
            var adverts = await _advertService.List();
            return Json(new { result = adverts, count = adverts.Count() });

        }

        [HttpGet("AddAdvert")]
        public async Task<IActionResult> AddAdvert()
        {
            var categoryFeatures = await _categoryFeaturesService.List();
            var categories = await _categoryService.List();
            var advert = new AdvertAddModel
            {
                CategoryId = categories.First().Id,
                Categories = categories,
                categoryFeaturesId = categoryFeatures.First().Id,
                categoryFeaturesListModels = categoryFeatures
            };
            return View(advert);
        }

        [HttpPost("GetCategories")]
        public async Task<IActionResult> GetCategories([FromBody] DataManagerRequest dm)
        {
            var categories = await _categoryService.List();
            return Json(categories);
        }

        [HttpPost("AddAdvert")]
        public async Task<IActionResult> AddAdvert(AdvertAddModel advertAdd, IFormFileCollection formFiles)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim.Value, out int userId)) return BadRequest("Geçersiz Kullanıcı");
            advertAdd.UserId = userId;
            var newAdvert = await _advertService.Add(advertAdd, formFiles);
            return RedirectToAction("Index", "Advert");
        }
        [HttpPost("GetCategoryFeaturesById/{categoryId}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> GetCategoryFeaturesById(int categoryId)
        {
            var features = await _categoryFeaturesService.GetByCategoryIdAsync(categoryId);
            return Json(features);
        }

    }
}
