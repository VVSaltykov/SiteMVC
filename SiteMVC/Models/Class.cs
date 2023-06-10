using System.ComponentModel.DataAnnotations;

namespace SiteMVC.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Номер класса")]
        public string? Name { get; set; }
        public int? UserId { get; set; }
        public List<Users>? Users { get; set; }
        public List<Lesson>? Lessons { get; set; }
        public List<HomeWork>? HomeWorks { get; set; }
    }
}
