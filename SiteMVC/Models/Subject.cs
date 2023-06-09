namespace SiteMVC.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<Journal> Journals { get; set; }
    }
}
