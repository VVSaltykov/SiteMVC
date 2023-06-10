using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace SiteMVC.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Display(Name = "ФИО")]
        public string? FIO { get; set; }
        [Display(Name = "Телефон")]
        public string? Phone { get; set; }
        public int? ClassId { get; set; }
        public int? RoleId { get; set; }
        public int? AccountId { get; set; }
        [ForeignKey("RoleId")]
        public Roles? Roles { get; set; }
        [ForeignKey("AccountId")]
        public Account? Account { get; set; }
        public List<Class>? Classes { get; set; } = null;
        public List<Lesson>? Lessons { get; set; }
        public List<Journal>? Journals { get; set; }
    }
}
