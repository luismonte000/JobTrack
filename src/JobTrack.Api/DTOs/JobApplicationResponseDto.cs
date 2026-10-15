namespace JobTrack.Api.DTOs;

public class JobApplicationResponseDto
{
    public Guid Id { get; set; }

    public Guid? CompanyId { get; set; }

    public string JobTitle { get; set; } = string.Empty;

    public string? Location { get; set; }

    public string? WorkMode { get; set; }

    public decimal? Salary { get; set; }

    public string? SalaryCurrency { get; set; }

    public string? JobUrl { get; set; }

    public string? JobDescription { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime? AppliedAt { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}