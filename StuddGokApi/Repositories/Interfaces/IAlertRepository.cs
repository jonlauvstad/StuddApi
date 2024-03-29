﻿using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface IAlertRepository
{
    Task<IEnumerable<Alert>> GetAlertsByUserIdAsync(int userId, bool seen);
    Task<IEnumerable<Alert>?> UpdateUnseenAlertsByUserIdAsync(int userId);
    Task<IEnumerable<Alert>?> UpdateAlertsByAlertIdsAsync(IEnumerable<int> alertIds);
    Task<bool> IsOwner(int userId, int alertId);
    Task<Alert?> UpdateAlertByIdAsync(int id);
}
