using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using System.Data;

namespace StuddGokApi.Services.Interfaces;

public interface IEventService
{
    Task<ICollection<EventDTO>?> GetEventsAsync(int userId, string type, DateTime? from, DateTime? to, int user_id, string role);
    Task<ICollection<EventDTO>> GetAllEventsAsync(string? type, DateTime? from, DateTime? to);
}
