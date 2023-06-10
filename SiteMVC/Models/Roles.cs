using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SiteMVC.Models
{
    public class Roles
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string? Name { get; set; }
        public List<Users>? Users { get; set; }
    }
}
