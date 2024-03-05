using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;

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
}
