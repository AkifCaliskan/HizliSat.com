using Newtonsoft.Json;
using Sahibinden.Entities.Concrete;
using System.Text.Json.Serialization;

namespace Sahibinden.Model.Advert
{
    public class AdvertAddModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public int AdvertId { get; set; }
        public List<IFormFile> AdvertImages { get; set; }
        public int CategoryId { get; set; }
        public string[]? AdvertImage { get; set; }
    }
}
