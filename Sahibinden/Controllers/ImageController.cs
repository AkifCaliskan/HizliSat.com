using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sahibinden.Business.Abstract;
using Sahibinden.Entities.Concrete;
using Sahibinden.Model.Category;
using Sahibinden.Model.Image;
using Sahibinden.Model.Upload;
using System.Runtime.CompilerServices;

namespace Sahibinden.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IConfiguration _configuration;
        private IImageService _imageService;
        public ImageController(IConfiguration configuration, IImageService imageService)
        {
            _configuration = configuration;
            _imageService = imageService;
        }

        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var path = Path.Combine(_configuration.GetValue<string>("RepoPath"));
            var fileExtension = Path.GetExtension(model.File.FileName);
            var newFileName = $"{DateTime.Now.ToString("ddMMyyyyHHmmss")}{fileExtension}";

            // Yükleme dizinini oluştur
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var filePath = Path.Combine(path, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            return Ok(new { FilePath = filePath });
        }
        [HttpPost]
        [Route("UploadImages")]
        public async Task<IActionResult> UploadImages([FromForm] List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files uploaded.");
            }

            var repoPath = _configuration.GetValue<string>("RepoPath");
            var relativePath = "Uploads"; // Dosyaların sunucuda saklanacağı alt dizin
            var path = Path.Combine(repoPath, relativePath);

            // Yükleme dizinini oluştur
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var uploadedFiles = new List<string>();

            foreach (var file in files)
            {
                var newFileName = Path.GetFileName(file.FileName); // Dosya adını al
                var filePath = Path.Combine(path, newFileName); // Sunucuda dosya yolu oluştur

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Dosya URL'si oluştur
                var fileUrl = $"{Request.Scheme}://{Request.Host}/{relativePath}/{newFileName}";
                uploadedFiles.Add(fileUrl); // URL'yi listeye ekle
            }

            return Ok(new { UploadedFiles = uploadedFiles });
        }

        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            IQueryable<Image> query = _imageService.GetQueryable(true);
            var model = query.Select(x => new ImageListModel
            {
                AdvertId = x.AdvertId,
                Images = x.Images
            });
            return Ok(model);
        }

    }
}
