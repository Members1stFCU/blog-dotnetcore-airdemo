using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using My.Feed.Providers.Messages;
using Newtonsoft.Json;

namespace My.Feed.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostingEnvironment _env;
        
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostingEnvironment env)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task Invoke(HttpContext context, IMessageProvider messageProvider)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                if (_env.IsProduction())
                {
                    messageProvider.AddMessage(new Message(MessageType.Error, "Internal Server Error"));
                }
                else
                {
                    messageProvider.AddException(ex);
                }

                _logger.LogCritical(ex, ex.Message);

                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var json = JsonConvert.SerializeObject(messageProvider.Messages);
                    await context.Response.WriteAsync(json);
                }
            }
        }
    }   
}
