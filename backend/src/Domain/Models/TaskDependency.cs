namespace QH.Domain.Models;

public class TaskDependency
{
    public int TaskId { get; set; }
    public int DependsOnTaskId { get; set; }
    public int DelayBuffer { get; set; } = 0;

    public Task Task { get; set; } = null!;
    public Task DependsOnTask { get; set; } = null!;
}
