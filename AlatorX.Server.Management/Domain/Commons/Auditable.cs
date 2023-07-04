using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatorX.Server.Management.Domain.Commons
{
    public class Auditable
    {
        public long? Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}