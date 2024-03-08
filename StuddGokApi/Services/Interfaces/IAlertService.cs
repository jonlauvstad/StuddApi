using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Services.Interfaces;

public interface IAlertService
{
    Task<IEnumerable<AlertDTO>> GetAlertsByUserIdAsync(int userId, bool seen);
    Task<IEnumerable<AlertDTO>?> UpdateUnseenAlertsByUserIdAsync(int userId);
    Task<IEnumerable<AlertDTO>?> UpdateAlertsByAlertIdsAsync(IEnumerable<int> alertIds, int userId, string role);
}
