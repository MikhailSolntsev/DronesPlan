namespace DronesPlan.Domain.Interfaces;

/// <summary>
/// Данные задачи, полученные из Jira.
/// </summary>
public record JiraIssueData(
    string Key,
    string Summary,
    string Status,
    string IssueType,
    string? Assignee,
    string JiraUrl
);

/// <summary>
/// Клиент для работы с Jira API.
/// </summary>
public interface IJiraClient
{
    /// <summary>
    /// Получить данные задач по списку ключей.
    /// </summary>
    Task<IReadOnlyList<JiraIssueData>> GetIssuesAsync(IEnumerable<string> issueKeys, CancellationToken ct = default);
}
