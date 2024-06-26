﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StuddGokApi.Configuration;
using StuddGokApi.SSE;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StuddGokApi.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class ServerSideEventController : ControllerBase
{
    private AlertUserList _alertUserList;
    private readonly ILogger<ServerSideEventController> _logger;
    private readonly IOptions<SseLogging> _sseLoggingConfig;

    public ServerSideEventController(AlertUserList alertUserList, ILogger<ServerSideEventController> logger, IOptions<SseLogging> sseLoggingConfig)
    {
        _alertUserList = alertUserList;
        _logger = logger;
        _sseLoggingConfig = sseLoggingConfig;
    }


    private void OnMessageReceived(object sender, ClientConnection clientConnection)
    {
        var responseStream = clientConnection.StreamWriter.BaseStream;
        string data = "Alert from API";
        var messageBytes = Encoding.UTF8.GetBytes($"data: {data}\n\n");
        responseStream.WriteAsync(messageBytes, 0, messageBytes.Length);
        responseStream.FlushAsync();
    }


    [HttpGet("{userId}", Name = "Register")]
    public async Task RegisterForSSE([FromRoute] int userId, CancellationToken cancellationToken)
    {
        Response.Headers.Add("Content-Type", "text/event-stream");

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_alertUserList.UserIdList.Contains(userId))
                {
                    string data = $"data: This is a message\n\n";
                    await Response.Body.WriteAsync(
                        Encoding.UTF8.GetBytes($"data: {data}"),
                        cancellationToken);
                    await Response.Body.FlushAsync();
                    _alertUserList.RemoveValueFromUserIdList(userId);
                }
                await Task.Delay(1000, cancellationToken);
            }
        }
        catch (Exception ex) 
        {
            if (_sseLoggingConfig.Value.On) _logger.LogDebug("ServerSideEventController RegisterForSSE ErrorMessage: {errMsg}", ex.Message);
        }



    }
}
