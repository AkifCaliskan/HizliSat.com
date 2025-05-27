using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Sahibinden.Business.Abstract;
using Sahibinden.Business.DTO_s;
using Sahibinden.Model.User;

namespace Sahibinden.AdminPanel.Controllers;
[Route("Auth")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ICacheService _cacheService;

    public AuthController(IHttpClientFactory httpClientFactory, ICacheService cacheService, IAuthService authService)
    {
        _httpClientFactory = httpClientFactory;
        _cacheService = cacheService;
        _authService = authService;
    }

    [HttpGet("Login")]
    [AllowAnonymous]
    public ViewResult Login()
    {
        return View();
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserLoginDetailModel model)
    {
        var login = await _authService.Authenticate(model);
        if (login is null) return View(model);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, login.Id.ToString()),
            new Claim(ClaimTypes.Email, login.Email),
            new Claim("UserId", login.Id.ToString()),
            new Claim(ClaimTypes.Role, login.UserType.ToString())
        };
        var identity = new ClaimsIdentity(claims, "AdminCookieAuth");
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync("AdminCookieAuth", principal);
        return RedirectToAction("Index", "Home");

    }
}
