using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatorX.Server.Management.Models
{
    public class Response
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}