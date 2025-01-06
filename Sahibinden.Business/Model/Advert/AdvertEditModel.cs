namespace Sahibinden.Model.Advert
{
    public class AdvertEditModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public List<AdvertDetails>? AdvertDetails { get; set; }
        public List<AdvertImage>? AdvertImages { get; set; }
    }
    public class AdvertDetails
    {
        public int Id { get; set; }
      
    }
    public class AdvertImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
}
