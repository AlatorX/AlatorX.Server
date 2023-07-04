namespace AlatorX.Server.Gateway.Models;

public class UserToken : Auditable
{
    public long UserId { get; set; }
    public string ApiKey { get; set; }
}
