using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SiteMVC.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Display(Name = "Название предмета")]
        public string? Name { get; set; }
        public List<Lesson>? Lessons { get; set; }
        public List<Journal>? Journals { get; set; }
    }
}
