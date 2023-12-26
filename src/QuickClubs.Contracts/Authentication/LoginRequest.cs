using System.ComponentModel.DataAnnotations;

namespace QuickClubs.Contracts.Authentication;

public sealed class LoginRequest
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
