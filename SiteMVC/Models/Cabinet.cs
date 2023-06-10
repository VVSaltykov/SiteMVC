using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SiteMVC.Models
{
    public class Cabinet
    {
        public int Id { get; set; }
        [Display(Name = "Номер кабинета")]
        public int? Number { get; set; }
        [Display(Name = "Описание")]
        public string? Description { get; set; }
        public List<Lesson>? Lessons { get; set; }
    }
}
