using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;
using System.Data;

namespace StuddGokApi.Services;

public class AlertService : IAlertService
{
    private readonly IAlertRepository _alertRepository;
    private readonly IMapper<Alert, AlertDTO> _alertMapper;
    private readonly ILogger<AlertService> _logger; 

    public AlertService(IAlertRepository alertRepository, IMapper<Alert, AlertDTO> alertMapper, ILogger<AlertService> logger)
    {
        _alertRepository = alertRepository;
        _alertMapper = alertMapper;
        _logger = logger;
    }
    public async Task<IEnumerable<AlertDTO>> GetAlertsByUserIdAsync(int userId, bool seen)
    {
        return from alert in await _alertRepository.GetAlertsByUserIdAsync(userId, seen) select _alertMapper.MapToDTO(alert);
    }

    public async Task<AlertDTO?> UpdateAlertByIdAsync(int id, int user_id)
    {
        if (!await _alertRepository.IsOwner(user_id, id)) return null;
        Alert? alert = await _alertRepository.UpdateAlertByIdAsync(id);
        if (alert == null) return null;
        return _alertMapper.MapToDTO(alert);
    }

    public async Task<IEnumerable<AlertDTO>?> UpdateAlertsByAlertIdsAsync(IEnumerable<int> alertIds, int userId, string role)
    {
        if (role != "admin")
        {
            foreach (int alertId in alertIds)
            {
                if (!await _alertRepository.IsOwner(userId, alertId)) return null;
            }
        }

        IEnumerable<Alert>? alerts = await _alertRepository.UpdateAlertsByAlertIdsAsync(alertIds);
        if (alerts == null) return null;
        return from alert in alerts select _alertMapper.MapToDTO(alert);
    }

    public async Task<IEnumerable<AlertDTO>?> UpdateUnseenAlertsByUserIdAsync(int userId)
    {
        IEnumerable<Alert>? alerts = await _alertRepository.UpdateUnseenAlertsByUserIdAsync(userId);
        if (alerts == null) return null;
        return from alert in alerts select _alertMapper.MapToDTO(alert);

    }
}
