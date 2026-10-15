namespace JobTrack.Api.Entities;

public class JobApplication
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }

    public Guid? CompanyId { get; set; }

    public string JobTitle { get; set; } = string.Empty;

    public string? Location { get; set; }

    public string? WorkMode { get; set; }

    public decimal? Salary { get; set; }

    public string? SalaryCurrency { get; set; }

    public string? JobUrl { get; set; }

    public string? JobDescription { get; set; }

    public string Status { get; set; } = "Interested";

    public DateTime? AppliedAt { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}