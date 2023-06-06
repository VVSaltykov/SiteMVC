namespace SiteMVC.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int Grade { get; set; }
        public string WorkType { get; set; }
        public int Pass { get; set; }
        public Account User { get; set; }
        public Lesson Lesson { get; set; }
    }
}
