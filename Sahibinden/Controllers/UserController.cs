using Microsoft.AspNetCore.Mvc;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Concrete.Services;
using Sahibinden.Entities.Concrete;

namespace Sahibinden.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _userService.List();
                return Ok(users);
            }
            catch (Exception)
            {

                ResultWrapperService<User>.FailureResult("Hata");
            }
            return Ok();
        }
    }
}