using System;
using System.Net;

namespace WebApi.Infrastructure
{
    public class WebApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public WebApiException(HttpStatusCode p_statusCode, string p_message) 
        : base(p_message)
        {
            this.StatusCode = p_statusCode;
        }

        public WebApiException(int p_statusCode, string p_message) 
        : base(p_message)
        {
            this.StatusCode = (HttpStatusCode)p_statusCode;
        }

         public WebApiException(string p_message) 
        : base(p_message)
        {
            this.StatusCode = HttpStatusCode.InternalServerError;
        }
    }
}