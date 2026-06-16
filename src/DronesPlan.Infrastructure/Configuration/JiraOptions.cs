namespace DronesPlan.Infrastructure.Configuration;

public class JiraOptions
{
    public const string SectionName = "Jira";

    public string BaseUrl { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string ApiToken { get; set; } = string.Empty;
}
