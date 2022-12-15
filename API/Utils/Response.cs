using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Utils
{
    public class Response
    {
        public Response(bool success, object data, ResponseError error)
        {
            this.Success = success;
            this.Data = data;
            this.Error = error;
        }

        public string ApiVersion { get; set; } = "1.0";
        public bool Success { get; set; }
        public object Data { get; set; }
        public ResponseError Error { get; set; }
    }
}