using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Model.User;
using SahibindenUi.Pages.Shared;
using Syncfusion.EJ2.Base;

namespace Sahibinden.AdminPanel.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserService _userService;

        public UserController(IHttpClientFactory httpClientFactory, IUserService userService)
        {
            _httpClientFactory = httpClientFactory;
            _userService = userService;
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

        

        [HttpPost("GetUsers")]
        public async Task<IActionResult> GetUsers([FromBody]DataManagerRequest model)
        {

            var users = await _userService.List();
            if (users == null)
            {
                return BadRequest("Kullanıcılar bulunamadı");
            }
            return Json(new { result= users  ,count = users.Count()} );
            
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] Syncfusion.EJ2.Base.CRUDModel<UserRegisterModel> request)
        {
            if (request?.Value == null)
            {
                Console.WriteLine("Model NULL geldi!");
                return BadRequest("Geçersiz Veri");
            }
            Console.WriteLine("Gelen veri: " + JsonConvert.SerializeObject(request.Value));
            var newUser = await _userService.Add(request.Value);
            return Json(newUser);

            
        }
    }
}
