namespace DronesPlan.DAL.Entities;

/// <summary>
/// Снапшот задачи Jira в момент синхронизации.
/// </summary>
public class IssueSnapshot
{
    public int Id { get; set; }
    
    /// <summary>
    /// Ключ задачи в Jira (например, PROJ-123).
    /// </summary>
    public string IssueKey { get; set; } = string.Empty;
    
    /// <summary>
    /// Заголовок задачи.
    /// </summary>
    public string Summary { get; set; } = string.Empty;
    
    /// <summary>
    /// Статус задачи в Jira.
    /// </summary>
    public JiraStatus Status { get; set; }
    
    /// <summary>
    /// Тип задачи.
    /// </summary>
    public IssueType IssueType { get; set; }    
    /// <summary>
    /// Текущий исполнитель (assignee) из Jira.
    /// </summary>
    public string? Assignee { get; set; }
    
    /// <summary>
    /// Вычисленный приоритет 0-100.
    /// </summary>
    public int Priority { get; set; }
    
    /// <summary>
    /// Цвет для отображения (blue, orange, grey).
    /// </summary>
    public string Color { get; set; } = "grey";
    
    /// <summary>
    /// URL задачи в Jira.
    /// </summary>
    public string JiraUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Ссылка на батч.
    /// </summary>
    public int SnapshotBatchId { get; set; }
    public SnapshotBatch? SnapshotBatch { get; set; }
}
