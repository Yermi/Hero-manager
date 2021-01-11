using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;

namespace WebApi.Infrastructure
{
    public class InterceptionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var dfds = request.Body.ToString();
            var ipAddres = context.HttpContext.Connection.RemoteIpAddress.ToString();

            var requestInfo = new
            {
                HttpMethod = request.Method,
                Uri = request.GetEncodedUrl(),
                IpAddress = ipAddres
            };

            var json = JsonConvert.SerializeObject(requestInfo, Formatting.Indented);
            Logger.Log.Debug($"HTTP Request {json}");
            base.OnActionExecuting(context);
        }
    }
}