namespace Sahibinden.Business.Model.Advert
{
    public class AdvertEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool Status { get; set; }
        public DateTime UpdatedTime { get; set; }
        
    }
   
}
