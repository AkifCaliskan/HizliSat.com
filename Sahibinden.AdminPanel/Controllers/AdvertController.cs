using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Model.Advert;
using Sahibinden.Entities.Concrete;
using Syncfusion.EJ2.Base;

namespace Sahibinden.AdminPanel.Controllers
{
    public class AdvertController : Controller
    {
        private readonly IAdvertService _advertService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICategoryService _categoryService;

        public AdvertController(IAdvertService advertService, IHttpClientFactory httpClientFactory)
        {
            _advertService = advertService;
            _httpClientFactory = httpClientFactory;
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


        [HttpPost("AddAdvert")]
        public async Task<IActionResult> AddAdvert([FromBody] AdvertAddModel advertAdd)
        {
            var newAdvert = await _advertService.Add(advertAdd);
            return Ok(newAdvert);
        }

    }
}
