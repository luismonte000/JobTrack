namespace JobTrack.Api.Entities;

public class ApplicationStatusHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid JobApplicationId { get; set; }

    public string? PreviousStatus { get; set; }

    public string NewStatus { get; set; } = string.Empty;

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}