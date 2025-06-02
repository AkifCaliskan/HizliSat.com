using Newtonsoft.Json;
using Sahibinden.Business.Model.AdvertDetail;
using Sahibinden.Business.Model.Category;
using Sahibinden.Business.Model.CategoryFeatures;
using Sahibinden.Entities.Concrete;


namespace Sahibinden.Business.Model.Advert
{
    public class AdvertAddModel
    {
        [JsonProperty("Name")]

        public string Name { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public AdvertDetailAdd AdvertDetailAddModel { get; set; }
        public Dictionary<int, string> FeatureValues { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CategoryId { get; set; }
        public int ParentId { get; set; }
        public string CategoryName { get; set; }
        public List<CategoryListModel> Categories { get; set; }
        public int categoryFeaturesId { get; set; }
        public List<CategoryFeaturesListModel> categoryFeaturesListModels { get; set; }
        public List<string> ImagePath { get; set; }


    }
}
