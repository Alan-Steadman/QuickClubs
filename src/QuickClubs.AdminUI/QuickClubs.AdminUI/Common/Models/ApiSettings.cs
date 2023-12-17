using System.ComponentModel.DataAnnotations;

namespace QuickClubs.AdminUI.Common.Models;

internal sealed class ApiSettings
{
    public const string SectionName = nameof(ApiSettings);

    [Required]
    public string BaseUrl { get; init; } = null!;

    [Required]
    public string UserAgent { get; init; } = null!;
}
