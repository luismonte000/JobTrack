using JobTrack.Api.Models;
namespace JobTrack.Api.Entities;

public class Company
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Website { get; set; }

    public string? Location { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ApplicationUser User { get; set; } = null!;
}