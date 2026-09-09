namespace QH.Domain.Models;

public class Task
{
    public int Id { get; set; }
    public int HouseId { get; set; }
    public int? CraftsmanId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateOnly? BaselineStartDate { get; set; }
    public DateOnly? BaselineEndDate { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public House House { get; set; } = null!;
    public User? Craftsman { get; set; }
    public ICollection<TaskDependency> Dependencies { get; set; } = new List<TaskDependency>();
    public ICollection<TaskDependency> Dependents { get; set; } = new List<TaskDependency>();
    public ICollection<ClientDecision> ClientDecisions { get; set; } = new List<ClientDecision>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Media> Media { get; set; } = new List<Media>();
}
