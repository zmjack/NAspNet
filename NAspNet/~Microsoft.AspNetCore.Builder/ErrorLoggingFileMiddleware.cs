using NAspNet;
using Microsoft.AspNetCore.Http;
using NStandard.Locks;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public class ErrorLoggingFileMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string PathBase;

        private static readonly TypeLockParser LockParser = new TypeLockParser($"{nameof(NAspNet)}:{nameof(ErrorLoggingFileMiddleware)}");
        private static FileStream _loggingFileStream;
        private static StreamWriter _loggingWriter;
        private string CheckHourlyFile;

        public ErrorLoggingFileMiddleware(RequestDelegate next, string pathBase)
        {
            _next = next;
            PathBase = pathBase;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                using (LockParser.Parse<ErrorLoggingFileMiddleware>().Begin())
                {
                    var now = DateTime.Now;
                    var dailyDir = now.ToString("yyyyMMdd");
                    var hourlyFile = $"{dailyDir}/{now:yyyyMMdd} {now.Hour:00}h-{(now.Hour + 1) % 24:00}h.txt";

                    if (CheckHourlyFile != hourlyFile)
                    {
                        var dir = $"{PathBase}/{dailyDir}";
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);

                        CheckHourlyFile = hourlyFile;

                        _loggingFileStream?.Dispose();
                        _loggingWriter?.Dispose();

                        _loggingFileStream = new FileStream($"{PathBase}/{hourlyFile}", FileMode.Append, FileAccess.Write);
                        _loggingWriter = new StreamWriter(_loggingFileStream);
                    }

                    _loggingWriter.WriteLine($"{DateTime.Now}\tUrl: {context.Request.Url()}\tFrom: {context.Connection.RemoteIpAddress}");
                    _loggingWriter.WriteLine(ex.Message);
                    _loggingWriter.WriteLine(ex.StackTrace);
                    _loggingWriter.WriteLine();

                    _loggingWriter.Flush();
                    _loggingFileStream.Flush();
                }
                throw;
            }
        }
    }

    public static class ErrorLoggingFileMiddlewareExtension
    {
        public static IApplicationBuilder UseErrorLoggingFile(this IApplicationBuilder builder, string pathBase = "logs")
        {
            return builder.UseMiddleware<ErrorLoggingFileMiddleware>(pathBase);
        }
    }

}
