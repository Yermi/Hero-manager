using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Infrastructure
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";

            if (context.Exception is WebApiException wae)
            {
                context.HttpContext.Response.StatusCode = (int)wae.StatusCode;
                context.Result = new JsonResult(new
                {
                    Error = context.Exception.Message
                });
                return;
            }

            var code = HttpStatusCode.InternalServerError;
            context.HttpContext.Response.StatusCode = (int)code;
            context.Result = new JsonResult(new
            {
                Error = new[] { context.Exception.Message },
                StackTrace = context.Exception.StackTrace
            });
        }
    }
}