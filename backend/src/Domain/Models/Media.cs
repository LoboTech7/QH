namespace QH.Domain.Models;

public class Media
{
    public int Id { get; set; }
    public int HouseId { get; set; }
    public int UploadedByUserId { get; set; }
    public int? TaskId { get; set; }
    public int? ItemId { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public House House { get; set; } = null!;
    public User UploadedByUser { get; set; } = null!;
    public Task? Task { get; set; }
    public Item? Item { get; set; }
}
