using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;
using StuddGokApi.SSE;
using System.Data;

namespace StuddGokApi.Services;

public class AlertService : IAlertService
{
    private readonly IAlertRepository _alertRepository;
    private readonly IMapper<Alert, AlertDTO> _alertMapper;
    private AlertUserList _alertUserList;
    private readonly ILogger<AlertService> _logger; 

    public AlertService(IAlertRepository alertRepository, IMapper<Alert, AlertDTO> alertMapper, AlertUserList alertUserList,
        ILogger<AlertService> logger)
    {
        _alertRepository = alertRepository;
        _alertMapper = alertMapper;
        _alertUserList = alertUserList;
        _logger = logger;
    }
    public async Task<IEnumerable<AlertDTO>> GetAlertsByUserIdAsync(int userId, bool seen)
    {
        _logger.LogDebug("GETTING IT");
        _alertUserList.RemoveValueFromUserIdList(userId, all:true);
        _logger.LogDebug($"REMOVED {userId}");
        return from alert in await _alertRepository.GetAlertsByUserIdAsync(userId, seen) select _alertMapper.MapToDTO(alert);
    }

    public async Task<AlertDTO?> UpdateAlertByIdAsync(int id, int user_id)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;


        if (!await _alertRepository.IsOwner(user_id, id))
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "AlertService", "UpdateAlertByIdAsync", "_alertRepository.IsOwner returns false", traceId);

            return null;
        }

        Alert? alert = await _alertRepository.UpdateAlertByIdAsync(id);

        if (alert == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "AlertService", "UpdateAlertByIdAsync", "_alertRepository.UpdateAlertByIdAsync returns null", traceId);

            return null;
        }

        return _alertMapper.MapToDTO(alert);
    }

    public async Task<IEnumerable<AlertDTO>?> UpdateAlertsByAlertIdsAsync(IEnumerable<int> alertIds, int userId, string role)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;


        if (role != "admin")
        {
            foreach (int alertId in alertIds)
            {
                if (!await _alertRepository.IsOwner(userId, alertId))
                {
                    _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                        "AlertService", "UpdateAlertsByAlertIdsAsync", "_alertRepository.IsOwner returns false", traceId);
                    
                    return null;
                }
            }
        }

        IEnumerable<Alert>? alerts = await _alertRepository.UpdateAlertsByAlertIdsAsync(alertIds);
        if (alerts == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "AlertService", "UpdateAlertsByAlertIdsAsync", "_alertRepository.UpdateAlertsByAlertIdsAsync returns null", traceId);

            return null;
        }

        return from alert in alerts select _alertMapper.MapToDTO(alert);
    }

    public async Task<IEnumerable<AlertDTO>?> UpdateUnseenAlertsByUserIdAsync(int userId)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;

        IEnumerable<Alert>? alerts = await _alertRepository.UpdateUnseenAlertsByUserIdAsync(userId);
        if (alerts == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "AlertService", "UpdateUnseenAlertsByUserIdAsync", "_alertRepository.UpdateUnseenAlertsByUserIdAsync returns null", traceId);

            return null;
        }

        return from alert in alerts select _alertMapper.MapToDTO(alert);

    }
}
