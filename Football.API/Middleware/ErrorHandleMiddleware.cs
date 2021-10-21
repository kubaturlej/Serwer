using Football.Infrastructure.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Football.API.Middleware
{
    public class ErrorHandleMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandleMiddleware> _logger;

        public ErrorHandleMiddleware(ILogger<ErrorHandleMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (BadRequestException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
