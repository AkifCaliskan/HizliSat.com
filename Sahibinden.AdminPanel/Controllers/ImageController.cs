using Microsoft.AspNetCore.Mvc;

namespace Sahibinden.AdminPanel.Controllers
{
    [Route("Image")]
    public class ImageController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile uploadFiles)
        {
            if (uploadFiles != null && uploadFiles.Length > 0) {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder)) 
                    Directory.CreateDirectory(uploadsFolder);
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(uploadFiles.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadFiles.CopyToAsync(fileStream);
                }
                return Ok(new {ImagePath = "/uploads/" + uniqueFileName});
            }
            return BadRequest("Dosya Alınamadı");
        }
    }
}
