using Microsoft.AspNetCore.Http.HttpResults;
using System.Drawing;

namespace StuddGokApi.Extensions;

public static class LoggerExtension
{
    public static string Controller(this ILogger logger, string name, string url, string method,InOut inOut, 
        string traceId, int? statusCode = null)
    {
        if (statusCode==null) return $"{name}Controller /{url} {method} {inOut}\n\t\ttraceId: {traceId}";
        return $"{name}Controller /{url} {method} {inOut} {statusCode}\n\t\ttraceId: {traceId}"; 
    }
}

public enum InOut { In, Out }
