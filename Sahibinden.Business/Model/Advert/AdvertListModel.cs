namespace Sahibinden.Business.Model.Advert
{
    public class AdvertListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string RecordDate { get; set; }
        public string? FirstImage { get; set; }
        public int UserId { get; set; }
    }
}
