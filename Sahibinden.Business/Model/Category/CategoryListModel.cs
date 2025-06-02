using Sahibinden.Entities.Concrete;

namespace Sahibinden.Business.Model.Category
{
    public class CategoryListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public string RecordDate { get; set; }
        public bool hasChild { get; set; }

    }
}
