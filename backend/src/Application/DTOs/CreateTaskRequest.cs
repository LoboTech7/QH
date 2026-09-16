using System.ComponentModel.DataAnnotations;

namespace QH.Application.DTOs;

public class CreateTaskRequest
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateOnly StartDate { get; set; }

    [Required]
    [Range(1, 365, ErrorMessage = "DurationDays skal være mellem 1 og 365.")]
    public int DurationDays { get; set; }

    public int? CraftsmanId { get; set; }
}
