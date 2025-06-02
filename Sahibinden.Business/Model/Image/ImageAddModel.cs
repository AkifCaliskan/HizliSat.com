using Microsoft.AspNetCore.Http;

namespace Sahibinden.Business.Model.Image
{
    public class ImageAddModel
    {
        public IFormFile Images { get; set; }
        public int AdvertId { get; set; }
    }
}
