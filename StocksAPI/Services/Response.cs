using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class Response
    {
        public Response()
        {
        }

        public Response(int statusCode)
        {
            StatusCode = statusCode;
        }

        public Response(string msg, int statusCode)
        {
            Message = msg;
            StatusCode = statusCode;
        }

        public Response(string msg, int statusCode, bool error)
        {
            Message = msg;
            Error = error;
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public bool? Error { get; set; }
        public Exception exception { get; set; }
    }

    public class Response<T> : Response
    {
        public Response()
        {
        }

        public Response(int statusCode) : base(statusCode)
        {
        }

        public T Data { get; set; }
    }
}