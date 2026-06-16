using DronesPlan.Domain.Entities;

namespace DronesPlan.Domain.Configuration;

/// <summary>
/// Конфигурация для маппинга статусов и типов задач.
/// </summary>
public class StatusMappingOptions
{
    /// <summary>
    /// Маппинг строковых статусов Jira в приоритеты (0-100).
    /// </summary>
    public Dictionary<string, int> StatusPriorities { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    
    /// <summary>
    /// Маппинг строковых статусов Jira в цвета (blue, orange, grey).
    /// </summary>
    public Dictionary<string, string> StatusColors { get; set; } = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Значение приоритета по умолчанию, если маппинг не найден.
    /// </summary>
    public int DefaultPriority { get; set; } = 0;

    /// <summary>
    /// Цвет по умолчанию, если маппинг не найден.
    /// </summary>
    public string DefaultColor { get; set; } = "grey";
}
