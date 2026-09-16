namespace QH.Application.DTOs;

public class TaskDto
{
    public int Id { get; set; }
    public int HouseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? CraftsmanId { get; set; }
    public string? CraftsmanName { get; set; }

    public DateOnly StartDate { get; set; }
    public int DurationDays { get; set; }
    public DateOnly EndDate { get; set; }

    public DateOnly? BaselineStartDate { get; set; }
    public int? BaselineDurationDays { get; set; }
    public DateOnly? BaselineEndDate { get; set; }

    public int? DaysDelayed { get; set; }
    public bool IsCompleted { get; set; }
}
