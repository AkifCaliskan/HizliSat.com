namespace Sahibinden.Model.Category
{
    public class CategoryDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RecordDate { get; set; }
        public List<CategoryByAdvert> Adverts { get; set; }
    }
    public class CategoryByAdvert
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

    }
}
