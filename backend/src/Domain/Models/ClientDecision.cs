using QH.Domain.Enums;

namespace QH.Domain.Models;

public class ClientDecision
{
    public int Id { get; set; }
    public int HouseId { get; set; }
    public int? RelatedTaskId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateOnly Deadline { get; set; }
    public DecisionStatus Status { get; set; } = DecisionStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public House House { get; set; } = null!;
    public Task? RelatedTask { get; set; }
}
