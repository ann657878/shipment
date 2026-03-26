using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ShipmentTrackingApp.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "A global exception occurred.");

        if (context.HttpContext.Request.Path.StartsWithSegments("/api"))
        {
            context.Result = new ObjectResult(new { error = "An internal server error occurred." })
            {
                StatusCode = 500
            };
        }
        else
        {
            context.Result = new RedirectToActionResult("Error", "Home", null);
        }

        context.ExceptionHandled = true;
    }
}
