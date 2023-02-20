using System.Net;
using System.Text;
using Newtonsoft.Json;
using RadencyWebApi.Domain.Exceptions;

namespace RadencyWebApi.Presentation.Middlewares;

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorMiddleware> _logger;

    public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        object errors = null;
        
        var errorStringBody = new StringBuilder();
        errorStringBody.Append($"Utc DateTime: {DateTime.UtcNow}");
        errorStringBody.Append($"Http Method: {context.Request.Method}\n");
        errorStringBody.Append($"Request Headers: {context.Request.Headers}\n");
        errorStringBody.Append($"Request Query Params: {context.Request.QueryString}\n");
        errorStringBody.Append($"Request Body:\n{context.Request.Body}\n");
        _logger.LogError(errorStringBody.ToString());

        switch (ex)
        {
            case HttpException rest:
                errors = rest.Errors;
                context.Response.StatusCode = (int)rest.Code;
                break;

            case Exception e:
                errors = string.IsNullOrWhiteSpace(e.Message) ? "error" : e.Message;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        context.Response.ContentType = "application/json";

        if (errors != null)
        {
            var result = JsonConvert.SerializeObject(new
            {
                errors
            });

            await context.Response.WriteAsync(result);
        }
    }
}