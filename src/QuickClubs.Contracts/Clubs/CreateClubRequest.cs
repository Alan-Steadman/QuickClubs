using System.ComponentModel.DataAnnotations;

namespace QuickClubs.Contracts.Clubs;

public sealed class CreateClubRequest
{
    [Required]
    public string FullName { get; set; } = null!;
 
    [Required]
    public string Acronym { get; set; } = null!;

    [Required]
    public string Website { get; set; } = null!;
}