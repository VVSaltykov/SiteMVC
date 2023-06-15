using SiteMVC.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace SiteMVC.Models
{
    public class Journal
    {
        public int Id { get; set; }
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Display(Name = "Оценка")]
        public int? Grade { get; set; }
        [Display(Name = "Название работы")]
        public string WorkType { get; set; }
        public string? Presence { get; set; }
        public int? LessonID { get; set; }
        public int? UserID { get; set; }
        public int? SubjectId { get; set; }
        [ForeignKey("UserID")]
        public Users? User { get; set; }
        [ForeignKey("LessonID")]
        public Lesson? Lesson { get; set; }
        [ForeignKey("SubjectId")]
        public Subject? Subject { get; set; }
    }
}
