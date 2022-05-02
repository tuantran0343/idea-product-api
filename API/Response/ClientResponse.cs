using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Response
{
    public class ClientResponse<T>
    {
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public T Data { get; set; }
    }
}