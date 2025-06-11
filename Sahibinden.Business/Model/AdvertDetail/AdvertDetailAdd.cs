using Sahibinden.Entities.Concrete;

namespace Sahibinden.Business.Model.AdvertDetail

{
    public class AdvertDetailAdd
    {
        public int AdvertId { get; set; }
        public object Value { get; set; }
        public int CategoryFeatureId { get; set; }
        public List<CategoryFeature> CategoryFeatures { get; set; }
    }
}
