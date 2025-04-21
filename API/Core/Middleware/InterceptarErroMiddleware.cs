using System.Net;
using API.Core.Exceptions;
using Newtonsoft.Json;

namespace API.Core.Middleware
{
    public class InterceptarErroMiddleware
    {
        private readonly RequestDelegate next;

        public InterceptarErroMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, dynamic exception)
        {
            var code = HttpStatusCode.InternalServerError;

            if (exception is HttpErroDeUsuario)
                code = exception.codigo;

            Console.WriteLine(exception);
            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
