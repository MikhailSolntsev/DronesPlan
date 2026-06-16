using DronesPlan.Domain.Entities;

namespace DronesPlan.Domain.Interfaces;

/// <summary>
/// Репозиторий для работы со снапшотами задач (батчами).
/// </summary>
public interface ISnapshotRepository
{
    /// <summary>
    /// Сохраняет батч снапшотов.
    /// </summary>
    /// <param name="batch">Батч снапшотов для сохранения.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task SaveBatchAsync(SnapshotBatch batch, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает последний (самый свежий) батч снапшотов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Последний батч или null, если батчей нет.</returns>
    Task<SnapshotBatch?> GetLatestBatchAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает батч снапшотов по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор батча.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Батч с указанным идентификатором или null.</returns>
    Task<SnapshotBatch?> GetBatchByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает список последних батчей снапшотов.
    /// </summary>
    /// <param name="limit">Максимальное количество возвращаемых батчей.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список батчей.</returns>
    Task<IReadOnlyList<SnapshotBatch>> GetBatchesAsync(int limit, CancellationToken cancellationToken = default);
}
