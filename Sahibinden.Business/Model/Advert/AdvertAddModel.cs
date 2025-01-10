using Newtonsoft.Json;
using Sahibinden.Entities.Concrete;
using Sahibinden.Model.AdvertDetail;
using Sahibinden.Model.Category;
using Sahibinden.Model.Image;
using System.Text.Json.Serialization;

namespace Sahibinden.Business.Model.Advert
{
    public class AdvertAddModel
    {
        [JsonProperty("Name")]
        public int Id { get; set; }

        public string Name { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public int AdvertId { get; set; }
        public List<IFormFile> AdvertImages { get; set; }
        public int CategoryId { get; set; }
        public string[]? AdvertImage { get; set; }
      
    }
}
