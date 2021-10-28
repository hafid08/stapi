using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STApi.Models
{
    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
    public class ResponseSuccess : Response
    {
        public ResponseSuccess(string message = "")
        {
            Status = "success";
            Message = message;
        }
        public ResponseSuccess(dynamic payload)
        {
            Status = "success";
            Message = "";
            Data = payload;
        }

    }


    public class ResponseFailed : Response
    {
        public ResponseFailed(string message = "")
        {
            Status = "failed";
            Message = message;
        }
        public ResponseFailed(dynamic payload)
        {
            Status = "failed";
            Message = "";
            Data = payload;
        }

    }
}
