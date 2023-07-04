using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Domain.Commons;

namespace AlatorX.Server.Management.Domain.Entities
{
    public class Website : Auditable
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Port { get; set; }
    }
}