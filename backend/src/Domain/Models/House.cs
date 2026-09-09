namespace QH.Domain.Models;

public class House
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Status { get; set; } = "IN_PROGRESS";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User Client { get; set; } = null!;
    public ICollection<HousePm> ProjectManagers { get; set; } = new List<HousePm>();
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
    public ICollection<ClientDecision> ClientDecisions { get; set; } = new List<ClientDecision>();
    public ICollection<Item> Items { get; set; } = new List<Item>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Media> Media { get; set; } = new List<Media>();
}
