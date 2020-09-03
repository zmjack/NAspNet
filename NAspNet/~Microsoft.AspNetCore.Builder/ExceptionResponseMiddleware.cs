using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public class ExceptionResponseMiddleware<TException> where TException : Exception
    {
        private readonly RequestDelegate _next;
        private readonly Func<TException, string> _return;

        public ExceptionResponseMiddleware(RequestDelegate next, Func<TException, string> returnJson)
        {
            _next = next;
            _return = returnJson;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (TException ex)
            {
                context.Response.ContentType = "application/json; charset=utf-8";
                var json = _return(ex);
                await context.Response.WriteAsync(json);
            }
        }
    }

    public static class ErrorLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionResponseHandler<TException>(this IApplicationBuilder builder, Func<TException, string> returnJson)
            where TException : Exception
        {
            return builder.UseMiddleware<ExceptionResponseMiddleware<TException>>(returnJson);
        }
    }
}
