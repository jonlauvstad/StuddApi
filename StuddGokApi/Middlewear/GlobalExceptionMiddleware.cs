﻿namespace StuddGokApi.Middlewear;

public class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Noe gikk galt - test exception {@Machine} {@TraceId}",
                Environment.MachineName,
                System.Diagnostics.Activity.Current?.Id);

            await Results.Problem(
                title: "Noe fryktelig har skjedd!!",
                statusCode: StatusCodes.Status500InternalServerError,
                extensions: new Dictionary<string, Object?>
                {
                    { "traceId", System.Diagnostics.Activity.Current?.Id },
                })
                .ExecuteAsync(context);
        }
    }
}
