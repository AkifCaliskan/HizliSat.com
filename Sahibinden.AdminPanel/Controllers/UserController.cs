using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sahibinden.AdminPanel.Models.User;
using Sahibinden.Business.Model.User;
using Sahibinden.Entities.Enums;
using SahibindenUi.Pages.Shared;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sahibinden.AdminPanel.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            var model = new _LoginSignupFormModel
            {
                IsLogin = true
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Sahibinden.AdminPanel.Models.User.UserRegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }
            registerModel.UserType = UserType.Admin;
            var httpClient = _httpClientFactory.CreateClient();

            var json = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://localhost:44384/api/User", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kayıt başarısız. Lütfen tekrar deneyin.");
                return View(registerModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:44384/api/User");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<UserListModel>>(jsonString);
                return View("/UserManagement/Index", users);
            }

            ModelState.AddModelError(string.Empty, "Kullanıcılar alınamadı.");
            return View("../UserManagement/Index", new List<UserListModel>());
        }
    }
}
