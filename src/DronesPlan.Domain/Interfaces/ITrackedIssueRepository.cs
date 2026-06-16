using DronesPlan.Domain.Entities;

namespace DronesPlan.Domain.Interfaces;

/// <summary>
/// Репозиторий для работы с отслеживаемыми вручную задачами.
/// </summary>
public interface ITrackedIssueRepository
{
    /// <summary>
    /// Возвращает список всех активных отслеживаемых задач.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список активных отслеживаемых задач.</returns>
    Task<IReadOnlyList<TrackedIssue>> GetAllActiveAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает отслеживаемую задачу по её ключу.
    /// </summary>
    /// <param name="issueKey">Ключ задачи.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Отслеживаемая задача или null.</returns>
    Task<TrackedIssue?> GetByIssueKeyAsync(string issueKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет новую отслеживаемую задачу.
    /// </summary>
    /// <param name="trackedIssue">Задача для добавления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task AddAsync(TrackedIssue trackedIssue, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет существующую отслеживаемую задачу.
    /// </summary>
    /// <param name="trackedIssue">Задача для обновления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task UpdateAsync(TrackedIssue trackedIssue, CancellationToken cancellationToken = default);
}
