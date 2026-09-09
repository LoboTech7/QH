namespace QH.Domain.Models;

public class Item
{
    public int Id { get; set; }
    public int HouseId { get; set; }
    public int? ParentItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Supplier { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public House House { get; set; } = null!;
    public Item? ParentItem { get; set; }
    public ICollection<Item> SubItems { get; set; } = new List<Item>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Media> Media { get; set; } = new List<Media>();
}
