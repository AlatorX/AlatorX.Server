using System.ComponentModel.DataAnnotations;

namespace AlatorX.Server.Gateway.Models
{
    public class Website : Auditable
    {
        [DataType(DataType.Url)]
        public string Name { get; set; }

        public string? Domain { get; set; }
        public string? Port { get; set; }

        public string? ConfigString { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}
