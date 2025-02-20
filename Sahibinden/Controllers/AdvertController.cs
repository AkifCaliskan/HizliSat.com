using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Concrete.Services;
using Sahibinden.Business.Model.Advert;
using Sahibinden.Core.EntityFramework;
using Sahibinden.Core.EntityFramework;
using Sahibinden.Entities.Concrete;
using Sahibinden.Model.Advert;
using Sahibinden.Model.AdvertDetail;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http;
using System.Security.Claims;

namespace Sahibinden.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private IAdvertService _advertService;
        private IAdvertDetailService _advertDetailServce;
        private IImageService _imageServce;
        private IConfiguration _configuration;
        private IHttpContextAccessor _contextAccessor;
        public AdvertController(IAdvertService advertService, IAdvertDetailService advertDetailServce, IImageService imageServce, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _advertService = advertService;
            _advertDetailServce = advertDetailServce;
            _imageServce = imageServce;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
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
        [HttpPost]
        [Route("AdvertAdd")]
        public async Task<IActionResult> Add(Sahibinden.Business.Model.Advert.AdvertAddModel advertAddModel)
        {
            try
            {
                var adverts = await _advertService.Add(advertAddModel);
                return Ok(adverts);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                 await _advertService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id ,Sahibinden.Business.Model.Advert.AdvertEditModel advertEditModel)
        {
            try
            {
                var updatedItem = await _advertService.Update(advertEditModel);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        public static string ImageUrl(string imageUrl, HttpRequest request)
        {
            var result = "";
            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                var splitUrl = imageUrl.Split("C:/Users/25ahm/OneDrive/Masaüstü/Resimler/");
                result = $"{request.Scheme}://{request.Host}/{splitUrl[1]}";
            }
            return result;
        }

        private string ImageUrls(string imageUrl)
        {
            var request = _contextAccessor.HttpContext?.Request;
            var result = "";
            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                var splitUrl = imageUrl.Split("C:/Users/25ahm/OneDrive/Masaüstü/Resimler/");
                result = $"{request.Scheme}://{request.Host}/{splitUrl[1]}";
            }
            return result;
        }

        private void DecodeToken(string token)
        {

        }
    }
}
