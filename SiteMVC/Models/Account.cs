namespace SiteMVC.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Users> Users { get; set; }
    }
}
