using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Concrete.Services;
using Sahibinden.Business.DTO_s;
using Sahibinden.DataAccess.UnitOfWork;
using Sahibinden.Entities.Concrete;
using Sahibinden.Model.User;
using System.Diagnostics.Eventing.Reader;

namespace Sahibinden.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public AuthController(ICacheService cacheService, IAuthService authService, IUnitOfWork unitOfWork, IUserService userService)
        {
            _cacheService = cacheService;
            _authService = authService;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDetailModel userLoginDetailModel)
        {
            var hashedPass = PasswordHelper.HashPassword("1234");

            if (userLoginDetailModel != null)
            {
                var user = await _authService.Authenticate(userLoginDetailModel);
                if (user != null)
                {
                    return Ok("Giriş İşlemi Başarılı");

                }

            }

            return BadRequest("Kullanıcı adı ve parola boş geçilemez");

        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] Sahibinden.Business.Model.User.UserRegisterModel userRegisterModel)
        {
            var user = await _authService.Register(userRegisterModel);
            return Ok();
        }
    }

}

