using DronesPlan.Domain.Configuration;
using DronesPlan.Domain.Entities;
using DronesPlan.Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace DronesPlan.Domain.Services;

/// <summary>
/// Реализация сервиса для оркестрации снапшотов задач Jira.
/// </summary>
public class SnapshotService : ISnapshotService
{
    private readonly ITrackedIssueRepository _trackedIssueRepository;
    private readonly IJiraClient _jiraClient;
    private readonly ISnapshotRepository _snapshotRepository;
    private readonly StatusMappingOptions _options;

    public SnapshotService(
        ITrackedIssueRepository trackedIssueRepository,
        IJiraClient jiraClient,
        ISnapshotRepository snapshotRepository,
        IOptions<StatusMappingOptions> options)
    {
        _trackedIssueRepository = trackedIssueRepository;
        _jiraClient = jiraClient;
        _snapshotRepository = snapshotRepository;
        _options = options.Value;
    }

    /// <summary>
    /// Выполняет синхронизацию активных отслеживаемых задач с Jira и создает новый батч снапшотов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Созданный батч снапшотов.</returns>
    public async Task<SnapshotBatch> SyncSnapshotsAsync(CancellationToken cancellationToken = default)
    {
        var trackedIssues = await _trackedIssueRepository.GetAllActiveAsync(cancellationToken);
        if (trackedIssues.Count == 0)
        {
            var emptyBatch = new SnapshotBatch { CreatedAt = DateTimeOffset.UtcNow };
            await _snapshotRepository.SaveBatchAsync(emptyBatch, cancellationToken);
            return emptyBatch;
        }

        var keys = trackedIssues.Select(t => t.IssueKey).ToList();
        var jiraData = await _jiraClient.GetIssuesAsync(keys, cancellationToken);

        var batch = new SnapshotBatch
        {
            CreatedAt = DateTimeOffset.UtcNow
        };

        foreach (var data in jiraData)
        {
            var trackedIssue = trackedIssues.FirstOrDefault(t => t.IssueKey == data.Key);

            var priority = GetPriority(data.Status, trackedIssue?.ManualPriority);
            var color = GetColor(data.Status);
            var statusEnum = ParseJiraStatus(data.Status);
            var issueTypeEnum = ParseIssueType(data.IssueType);

            var snapshot = new IssueSnapshot
            {
                IssueKey = data.Key,
                Summary = data.Summary,
                Status = statusEnum,
                IssueType = issueTypeEnum,
                Assignee = data.Assignee,
                JiraUrl = data.JiraUrl,
                Priority = priority,
                Color = color
            };

            batch.Snapshots.Add(snapshot);
        }

        await _snapshotRepository.SaveBatchAsync(batch, cancellationToken);
        return batch;
    }

    private int GetPriority(string status, int? manualPriority)
    {
        if (manualPriority.HasValue)
        {
            return manualPriority.Value;
        }

        if (_options.StatusPriorities.TryGetValue(status, out var priority))
        {
            return priority;
        }

        return _options.DefaultPriority;
    }

    private string GetColor(string status)
    {
        if (_options.StatusColors.TryGetValue(status, out var color))
        {
            return color;
        }

        return _options.DefaultColor;
    }

    private JiraStatus ParseJiraStatus(string status)
    {
        var normalized = status.Replace(" ", "").Replace("-", "");
        if (Enum.TryParse<JiraStatus>(normalized, true, out var parsed))
        {
            return parsed;
        }

        return JiraStatus.Open; // Фолбэк по умолчанию
    }

    private IssueType ParseIssueType(string issueType)
    {
        var normalized = issueType.Replace(" ", "").Replace("-", "");
        if (Enum.TryParse<IssueType>(normalized, true, out var parsed))
        {
            return parsed;
        }

        if (normalized.Equals("Bug", StringComparison.OrdinalIgnoreCase))
            return IssueType.Error;

        return IssueType.Task; // Фолбэк по умолчанию
    }
}
