using System.Diagnostics;

namespace StuddGokApi.Tools;

public static class LoggerHelper
{
    public static void LogWithTraceId(string message)
    {
        string traceId = Activity.Current?.Id ?? "Unknown Trace ID";
        Trace.WriteLine($"Trace ID: {traceId} - {message}");
    }
}
