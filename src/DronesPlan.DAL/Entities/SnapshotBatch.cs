namespace DronesPlan.DAL.Entities;

/// <summary>
/// Группа снапшотов задач, полученных в рамках одной синхронизации.
/// </summary>
public class SnapshotBatch
{
    public int Id { get; set; }
    
    /// <summary>
    /// Время создания батча (момент синхронизации).
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
    
    /// <summary>
    /// Снапшоты задач в этом батче.
    /// </summary>
    public ICollection<IssueSnapshot> Snapshots { get; set; } = new List<IssueSnapshot>();
}
