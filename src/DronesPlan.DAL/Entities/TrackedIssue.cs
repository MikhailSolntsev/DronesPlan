namespace DronesPlan.DAL.Entities;

/// <summary>
/// Задача, добавленная в отслеживание вручную.
/// </summary>
public class TrackedIssue
{
    public int Id { get; set; }
    
    /// <summary>
    /// Ключ задачи в Jira (например, PROJ-123).
    /// </summary>
    public string IssueKey { get; set; } = string.Empty;
    
    /// <summary>
    /// Дата добавления в отслеживание.
    /// </summary>
    public DateTimeOffset AddedAt { get; set; }
    
    /// <summary>
    /// Активна ли задача (false = удалена из отслеживания).
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Ручной приоритет для бэклога (0-49). Null = используется автоматический.
    /// </summary>
    public int? ManualPriority { get; set; }
    
    /// <summary>
    /// Заметка пользователя.
    /// </summary>
    public string? Notes { get; set; }
}
