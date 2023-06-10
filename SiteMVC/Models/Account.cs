using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SiteMVC.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Display(Name = "Логин")]
        public string Login { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        public List<Users>? Users { get; set; }
    }
}
