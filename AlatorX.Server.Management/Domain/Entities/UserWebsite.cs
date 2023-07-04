using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Domain.Commons;

namespace AlatorX.Server.Management.Domain.Entities
{
    public class UserWebsite : Auditable
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public long WebsiteSettingId { get; set; }
        public WebsiteSetting WebsiteSetting { get; set; }
    }
}