namespace AlatorX.Server.Gateway.Models
{
    public class User : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public ICollection<Website> Websites { get; set; }
    }
}
