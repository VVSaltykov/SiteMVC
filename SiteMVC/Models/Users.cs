namespace SiteMVC.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string? FIO { get; set; }
        public string? Phone { get; set; }
        public int? ClassId { get; set; }
        public Roles Roles { get; set; }
        public Account Account { get; set; }
        public List<Class>? Classes { get; set; }
    }
}
