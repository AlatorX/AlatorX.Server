using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatorX.Server.Management.Service.Exceptions
{
    public class AlatorException : Exception
    {   
        public int Code { get; set; }
        public AlatorException(int code = 500, string message = "Something went wrong")
            : base(message)
        {
            this.Code = code;
        }
    }
}