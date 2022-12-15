using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Utils
{
    public class ResponseError
    {
        public ResponseError(int status, string message)
        {
            this.Status = status;
            this.Message = message;
        }

        public int Status { get; set;}
        public string Message { get; set; }
       
    }
}