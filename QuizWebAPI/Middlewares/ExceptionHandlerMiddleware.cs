
using Microsoft.AspNetCore.Mvc.Abstractions;
using Models.Response;
using System.Net;

namespace QuizWebAPI.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
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
            catch (Exception ex) 
            {
                var serviceName = context.GetEndpoint()?.Metadata.GetMetadata<ActionDescriptor>()?.DisplayName;
                serviceName = serviceName != null ? serviceName : string.Empty;
                string errorText = $"{Guid.NewGuid()} -> {serviceName} : {ex.Message}";
                logger.LogError(ex, errorText);

                //Return error response
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new ErrorResponse(errorText));
            }
        }
    }
}
