namespace TravelCompany.API.DTOs.Tour;

public class AnhTourUpdateDto
{
    public int MaAnh { get; set; }
    public int MaTour { get; set; }
    public string DuongDanAnh { get; set; } = null!;
    public string? MoTaAnh { get; set; }
    public int ThuTu { get; set; }
}
