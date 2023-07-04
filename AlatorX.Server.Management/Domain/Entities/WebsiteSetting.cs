using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AlatorX.Server.Management.Domain.Commons;

namespace AlatorX.Server.Management.Domain.Entities
{
    public class WebsiteSetting : Auditable
    {
        public long WebsiteId { get; set; }
        public Website Website { get; set; }

        public string ConfigString { get; set; }
    }
}