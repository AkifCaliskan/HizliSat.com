using Sahibinden.Entities.Enums;

namespace Sahibinden.Business.Model.CategoryFeatures
{
    public class CategoryFeaturesListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int categoryId { get; set; }
        public InputType InputType { get; set; }
        public List<string> Options { get; set; }
    }
}
