namespace DronesPlan.DAL.Entities;

/// <summary>
/// Статус задачи в Jira.
/// </summary>
public enum JiraStatus
{
    Open,
    Analysis,
    Ready,
    InProgress,
    Review,
    Testing,
    Build,
    Regress,
    Release,
    Closed
}
