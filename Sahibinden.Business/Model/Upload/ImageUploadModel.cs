using Microsoft.AspNetCore.Http;

namespace Sahibinden.Model.Upload
{
    public class ImageUploadModel
    {
        public IFormFile File { get; set; }
    }
}
