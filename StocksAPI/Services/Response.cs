using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{

    public class Response<T>
    {
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

        public Response(T data, string msg, int statusCode)
        {
            Data = data;
            Message = msg;
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public bool? Error { get; set; }
    }
}
