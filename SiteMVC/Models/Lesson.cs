using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteMVC.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string? WeekDay { get; set; }
        public int? LessonNumber { get; set; }
        public int? ClassID { get; set; }
        public int? SubjectId { get; set; }
        public int? CabinetId { get; set; }
        public int? UserID { get; set; }
        public List<HomeWork>? HomeWorks { get; set; }
        public List<Journal>? Journals { get; set; }
        [ForeignKey("ClassID")]
        public Class? Class { get; set; }
        public Subject? Subject { get; set; }
        public Cabinet? Cabinet { get; set; }
        [ForeignKey("UserID")]
        public Users? Users { get; set; }
    }
}
