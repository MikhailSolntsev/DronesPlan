using DronesPlan.Domain.Entities;

namespace DronesPlan.Domain.Interfaces;

/// <summary>
/// Сервис для оркестрации снапшотов задач Jira.
/// </summary>
public interface ISnapshotService
{
    /// <summary>
    /// Выполняет синхронизацию активных отслеживаемых задач с Jira и создает новый батч снапшотов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Созданный батч снапшотов.</returns>
    Task<SnapshotBatch> SyncSnapshotsAsync(CancellationToken cancellationToken = default);
}
