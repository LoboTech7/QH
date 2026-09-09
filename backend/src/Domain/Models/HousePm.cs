namespace QH.Domain.Models;

public class HousePm
{
    public int HouseId { get; set; }
    public int PmId { get; set; }

    public House House { get; set; } = null!;
    public User Pm { get; set; } = null!;
}
