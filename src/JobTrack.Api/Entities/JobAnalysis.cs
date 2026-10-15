namespace JobTrack.Api.Entities;

public class JobAnalysis
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid JobApplicationId { get; set; }

    public decimal MatchPercentage { get; set; }

    public string MatchedSkills { get; set; } = "[]";

    public string MissingSkills { get; set; } = "[]";

    public string? Summary { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}