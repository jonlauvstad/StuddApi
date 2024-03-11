using System.Collections.Concurrent;

namespace StuddGokApi.SSE;

public class AlertUserList
{
    public ConcurrentBag<int> UserIdList { get; set; } = new ConcurrentBag<int>();
    //public ConcurrentBag<ClientConnection> ConnectionList { get; set; } = new ConcurrentBag<ClientConnection>();
    private ILogger<AlertUserList> _logger;

    public AlertUserList(ILogger<AlertUserList> logger)
    {
        _logger = logger;
    }

    public void RemoveValueFromUserIdList(int value, bool all=false)
    {
        ConcurrentBag<int> ints = new ConcurrentBag<int>();

        int number = UserIdList.Where(x => x == value).Count();
        if(!all) number = Math.Min(1, number);
        int removedItem;
        for (int i = 0; i<number; i++)
        {
            while (UserIdList.TryTake(out removedItem))
            {
                if (removedItem != value)
                {
                    ints.Add(removedItem);
                }
                else
                {
                    foreach (int i_ in ints) UserIdList.Add(i_);
                    break;
                    
                }
            }
        }
        
    }
}
