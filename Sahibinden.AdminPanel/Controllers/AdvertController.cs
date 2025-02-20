using Microsoft.AspNetCore.Mvc;
using Sahibinden.Business.Abstract;
using Sahibinden.Entities.Concrete;
using System.Net.Http;

namespace Sahibinden.AdminPanel.Controllers
{
    public class AdvertController : Controller
    {
        private readonly IAdvertService _advertService;
        private readonly IHttpClientFactory _httpClientFactory;

        public AdvertController(IAdvertService advertService, IHttpClientFactory httpClientFactory)
        {
            _advertService = advertService;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
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
       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var adverts = await _advertService.List(new Sahibinden.Business.Model.Advert.AdvertListModel());
                return Ok(adverts);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
