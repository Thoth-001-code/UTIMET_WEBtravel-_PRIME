namespace TravelCompany.API.DTOs.Tour;

public class AnhTourCreateDto
{
    public int MaTour { get; set; }
    public string DuongDanAnh { get; set; } = null!;
    public string? MoTaAnh { get; set; }
    public int ThuTu { get; set; } = 0;
}
