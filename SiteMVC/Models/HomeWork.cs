namespace SiteMVC.Models
{
    public class HomeWork
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Lesson Lesson { get; set; }
        public Class Class { get; set; }
    }
}
