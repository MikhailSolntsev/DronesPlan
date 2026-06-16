using DronesPlan.Domain.Entities;

namespace DronesPlan.Domain.Interfaces;

/// <summary>
/// Репозиторий для работы с плановыми назначениями.
/// </summary>
public interface IPlannedAssignmentRepository
{
    /// <summary>
    /// Возвращает список плановых назначений для указанной задачи.
    /// </summary>
    /// <param name="issueKey">Ключ задачи.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список плановых назначений.</returns>
    Task<IReadOnlyList<PlannedAssignment>> GetByIssueKeyAsync(string issueKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет новое плановое назначение.
    /// </summary>
    /// <param name="plannedAssignment">Плановое назначение для добавления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task AddAsync(PlannedAssignment plannedAssignment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет существующее плановое назначение.
    /// </summary>
    /// <param name="plannedAssignment">Плановое назначение для удаления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task DeleteAsync(PlannedAssignment plannedAssignment, CancellationToken cancellationToken = default);
}
