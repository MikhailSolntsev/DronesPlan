namespace DronesPlan.Infrastructure.Clients;

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using DronesPlan.Domain.Interfaces;
using DronesPlan.Infrastructure.Configuration;

public class JiraClient : IJiraClient
{
    private readonly HttpClient _httpClient;
    private readonly JiraOptions _options;

    public JiraClient(HttpClient httpClient, IOptions<JiraOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;

        if (!string.IsNullOrEmpty(_options.BaseUrl))
        {
            _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        }

        if (!string.IsNullOrEmpty(_options.Username) && !string.IsNullOrEmpty(_options.ApiToken))
        {
            var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_options.Username}:{_options.ApiToken}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);
        }
    }

    public async Task<IReadOnlyList<JiraIssueData>> GetIssuesAsync(IEnumerable<string> issueKeys, CancellationToken ct = default)
    {
        var keys = issueKeys.ToList();
        if (keys.Count == 0)
        {
            return Array.Empty<JiraIssueData>();
        }

        var jql = $"key in ({string.Join(",", keys)})";
        var url = $"/rest/api/2/search?jql={Uri.EscapeDataString(jql)}&fields=summary,status,issuetype,assignee";

        var response = await _httpClient.GetAsync(url, ct);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<JiraSearchResponse>(cancellationToken: ct);
        
        if (result?.Issues == null)
        {
            return Array.Empty<JiraIssueData>();
        }

        var baseUrl = _options.BaseUrl.TrimEnd('/');

        return result.Issues.Select(issue => new JiraIssueData(
            Key: issue.Key,
            Summary: issue.Fields?.Summary ?? string.Empty,
            Status: issue.Fields?.Status?.Name ?? string.Empty,
            IssueType: issue.Fields?.IssueType?.Name ?? string.Empty,
            Assignee: issue.Fields?.Assignee?.DisplayName,
            JiraUrl: $"{baseUrl}/browse/{issue.Key}"
        )).ToList();
    }

    // DTOs for Jira API response
    private class JiraSearchResponse
    {
        [JsonPropertyName("issues")]
        public List<JiraIssue>? Issues { get; set; }
    }

    private class JiraIssue
    {
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;

        [JsonPropertyName("fields")]
        public JiraFields? Fields { get; set; }
    }

    private class JiraFields
    {
        [JsonPropertyName("summary")]
        public string? Summary { get; set; }

        [JsonPropertyName("status")]
        public JiraStatusInfo? Status { get; set; }

        [JsonPropertyName("issuetype")]
        public JiraIssueTypeInfo? IssueType { get; set; }

        [JsonPropertyName("assignee")]
        public JiraUserInfo? Assignee { get; set; }
    }

    private class JiraStatusInfo
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    private class JiraIssueTypeInfo
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    private class JiraUserInfo
    {
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }
    }
}
