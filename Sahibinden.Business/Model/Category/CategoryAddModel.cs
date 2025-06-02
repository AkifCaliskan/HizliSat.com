namespace Sahibinden.Business.Model.Category
{
    public class CategoryAddModel
    {
        public bool CategoryStatus { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; }
    }
}
