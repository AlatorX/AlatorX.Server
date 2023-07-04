﻿namespace AlatorX.Server.Gateway.Models;

public class Auditable
{
    public long? Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
