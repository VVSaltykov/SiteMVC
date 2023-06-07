namespace SiteMVC.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public Roles Role { get; set; }
        public Account Account { get; set; }
        public List<Class>? Classes { get; set; }
    }
}
