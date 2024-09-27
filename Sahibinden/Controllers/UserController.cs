using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sahibinden.Business.Abstract;
using Sahibinden.Entities.Concrete;
using Sahibinden.Model.User;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Numerics;
using System.Security.Claims;
using System.Text;

namespace Sahibinden.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IUserService _userService;

        public UserController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDetailModel userModel)
        {
           
            var user = _userService.GetQueryable(true).FirstOrDefault(p => p.Email == userModel.Email); 
            if (user != null)
            {
                var token = GenerateJwtToken(userModel.Email, user.Id);
                return Ok(new { Token = token, userId= user.Id });
            }
            else
            {
                return BadRequest("Kullanıcı bulunamadı");
            }

        }

        private string GenerateJwtToken(string username, int userId)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Actor, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        [HttpPost]
        [Route("NewUser")]
        public IActionResult NewUser([FromBody] UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound("Kullanıcı bilgileri eksik.");
            }
            var newUser = _userService.Add(new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                RecordDate = DateTime.Now,
                Password = model.Password,
                
            });
            return Ok(newUser);
        }

    }

}