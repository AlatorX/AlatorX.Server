using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Models;
using AlatorX.Server.Management.Service.Exceptions;

namespace AlatorX.Server.Management.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (AlatorException exception)
            {
                context.Response.StatusCode = exception.Code;
                await context.Response.WriteAsJsonAsync(new Response
                {
                    Code = exception.Code,
                    Message = exception.Message
                });
            }
            catch (Exception exception)
            {
                this.logger.LogError($"{exception}\n\n");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new Response
                {
                    Code = 500,
                    Message = exception.Message
                });
            }
        }
    }
}