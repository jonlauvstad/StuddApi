using StuddGokApi.DTMs;
using StuddGokApi.DTOs;
using System.ComponentModel;

namespace StuddGokApi.Repositories.Interfaces;

public interface IEventRepository
{
    Task<ICollection<Event>> GetEventsAsync(int userId, string? type, DateTime? from, DateTime? to);
}
