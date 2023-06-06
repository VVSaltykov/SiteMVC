using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SiteMVC.Models
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        public string? WeekDay { get; set; }
        public int? LessonNumber { get; set; }
        public Class ClassID { get; set; }
        public Subject SubjectID { get; set; }
        public Cabinet? CabinetID { get; set; }
        public Account? UserID { get; set; }
    }
}
