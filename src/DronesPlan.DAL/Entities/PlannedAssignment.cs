namespace DronesPlan.DAL.Entities;

/// <summary>
/// Плановое назначение исполнителя на задачу на конкретную дату.
/// </summary>
public class PlannedAssignment
{
    public int Id { get; set; }
    
    /// <summary>
    /// Ключ задачи.
    /// </summary>
    public string IssueKey { get; set; } = string.Empty;
    
    /// <summary>
    /// Дата, на которую запланировано назначение.
    /// </summary>
    public DateOnly Date { get; set; }
    
    /// <summary>
    /// Планируемый исполнитель.
    /// </summary>
    public string Assignee { get; set; } = string.Empty;
    
    /// <summary>
    /// Когда создана запись.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
}
