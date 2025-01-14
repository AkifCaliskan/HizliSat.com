using Microsoft.AspNetCore.Http;

namespace Sahibinden.Model.AdvertDetail

{
    public class AdvertDetailAdd
    {
        public int AdvertId { get; set; }
        public List<IFormFile> AdvertImages { get; set; }
        public int CategoryId { get; set; }
    }
}
