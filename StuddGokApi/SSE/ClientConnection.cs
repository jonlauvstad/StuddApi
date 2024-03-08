namespace StuddGokApi.SSE;

public class ClientConnection
{
    public int ConnectionId { get; set; }
    public StreamWriter StreamWriter { get; set; }

    public ClientConnection(int connectionId, StreamWriter streamWriter)
    {
        ConnectionId = connectionId;
        StreamWriter = streamWriter;
    }
}
