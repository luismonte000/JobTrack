using System.ComponentModel.DataAnnotations;

namespace JobTrack.Api.DTOs;

public class CreateJobApplicationDto
{
    [Required]
    [StringLength(200)]
    public string JobTitle { get; set; } = string.Empty;

    public Guid? CompanyId { get; set; }

    [StringLength(200)]
    public string? Location { get; set; }

    [StringLength(20)]
    public string? WorkMode { get; set; }

    [Range(0, 9999999999.99)]
    public decimal? Salary { get; set; }

    [StringLength(3, MinimumLength = 3)]
    public string? SalaryCurrency { get; set; }

    [StringLength(2048)]
    [Url]
    public string? JobUrl { get; set; }

    public string? JobDescription { get; set; }

    [StringLength(2000)]
    public string? Notes { get; set; }
}