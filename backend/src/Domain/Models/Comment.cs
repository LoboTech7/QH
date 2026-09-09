namespace QH.Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int HouseId { get; set; }
    public int? TaskId { get; set; }
    public int? ItemId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
    public House House { get; set; } = null!;
    public Task? Task { get; set; }
    public Item? Item { get; set; }
}
