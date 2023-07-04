using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Domain.Commons;

namespace AlatorX.Server.Management.Service.DTOs.Users
{
    public class UserForResultDto : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}