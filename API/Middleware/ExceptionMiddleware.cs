using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
  public class ExceptionMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
      _env = env;
      _logger = logger;
      _next = next;
    }

    // 
    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context); // if no error --> pass through httpcontext; if error --> go to catch block
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, ex.Message); // use logger to log error
        context.Response.ContentType = "application/json"; // return smt to client
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // generate response
        var response = _env.IsDevelopment() // ternary (conditional) operator
          ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) // use ToString() to add new line break to exception --> more readable when display in browser
          : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

        // 
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);
      }
    }
  }
}