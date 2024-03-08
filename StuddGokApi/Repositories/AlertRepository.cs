using Microsoft.EntityFrameworkCore;
using StuddGokApi.Data;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;

namespace StuddGokApi.Repositories;

public class AlertRepository : IAlertRepository
{
    private readonly StuddGokDbContext _dbContext;
    private readonly ILogger<AlertRepository> _logger;

    public AlertRepository(StuddGokDbContext dbContext, ILogger<AlertRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task<IEnumerable<Alert>> GetAlertsByUserIdAsync(int userId, bool seen)
    {
        return await _dbContext.Alerts.Where(x => x.UserId == userId && x.Seen == seen).ToListAsync();
    }

    public async Task<IEnumerable<Alert>?> UpdateUnseenAlertsByUserIdAsync(int userId)
    {
        IEnumerable<Alert>? alerts = await _dbContext.Alerts.Where(x =>  userId == x.UserId && x.Seen == false).ToListAsync();

        var strategy = _dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (Alert alert in alerts)
                    {
                        alert.Seen = true;
                        await _dbContext.SaveChangesAsync();
                    }
                    transaction.Commit();

                }
                catch
                {
                    transaction.Rollback();
                    alerts = null;
                }
            }
        });
        return alerts;
    }

    public async Task<IEnumerable<Alert>?> UpdateAlertsByAlertIdsAsync(IEnumerable<int> alertIds)
    {
        IEnumerable<Alert>? alerts = await _dbContext.Alerts.Where(x => alertIds.Contains(x.Id)).ToListAsync();

        var strategy = _dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (Alert alert in alerts)
                    {
                        alert.Seen = true;
                        await _dbContext.SaveChangesAsync();
                    }
                    transaction.Commit();

                }
                catch
                {
                    transaction.Rollback();
                    alerts = null;
                }
            }
        });
        return alerts;
    }

    public async Task<bool> IsOwner(int userId, int alertId)
    {
        return  (await _dbContext.Alerts.Where(x => x.UserId == userId && x.Id == alertId).ToListAsync()).Count() > 0;
    }
}
