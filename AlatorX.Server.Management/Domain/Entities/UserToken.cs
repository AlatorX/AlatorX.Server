using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Domain.Commons;

namespace AlatorX.Server.Management.Domain.Entities
{
    public class UserToken : Auditable
    {
        public long UserId { get; set; }
        public string ApiKey { get; set; }
    }
}