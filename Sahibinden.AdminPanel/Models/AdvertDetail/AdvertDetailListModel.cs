namespace Sahibinden.Model.AdvertDetail
{
    public class AdvertDetailListModel
    {
        public int AdvertId { get; set; }
        public int Id { get; set; }
        public List<string> Images { get; set; }
        public string RecordDate { get; set; }
        public bool Status { get; set; }
    }
}
