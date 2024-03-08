namespace StuddGokApi.SSE;

public class AlertUserList
{
    public List<int> UserIdList { get; set; } = new List<int>();
    public List<ClientConnection> ConnectionList { get; set; } = new List<ClientConnection>();
}
