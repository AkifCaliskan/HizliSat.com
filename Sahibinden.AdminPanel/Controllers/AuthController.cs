using Microsoft.AspNetCore.Mvc;

using Sahibinden.Business.Abstract;
using Sahibinden.Business.DTO_s;
using Sahibinden.Model.User;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sahibinden.AdminPanel.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICacheService _cacheService;

        public AuthController(IHttpClientFactory httpClientFactory, ICacheService cacheService)
        {
            _httpClientFactory = httpClientFactory;
            _cacheService = cacheService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Lütfen tüm alanları doldurun.";
                return View("Index", model);
            }

            var client = _httpClientFactory.CreateClient();
            var jsonContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:44384/api/Auth/Login", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
                return View("Index", model);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content: " + responseContent);

            if (string.IsNullOrEmpty(responseContent))
            {
                ViewBag.Error = "Sunucudan boş yanıt alındı!";
                return View("Index", model);
            }

            var loginResponse = JsonSerializer.Deserialize<LoginResponseDto>(responseContent);
            if (loginResponse == null)
            {
                ViewBag.Error = "Yanıt deserialization hatası!";
                return View("Index", model);
            }

            _cacheService.SetToCache("AuthToken", loginResponse.Token, TimeSpan.FromHours(1));

            return RedirectToAction("Dashboard", "Home");
        }
    }
}
