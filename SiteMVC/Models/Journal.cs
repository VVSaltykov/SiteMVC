using System.ComponentModel.DataAnnotations.Schema;

namespace SiteMVC.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int Grade { get; set; }
        public string WorkType { get; set; }
        public int Pass { get; set; }
        public int LessonID { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public Users User { get; set; }
        [ForeignKey("LessonID")]
        public Lesson Lesson { get; set; }
    }
}
