namespace TravelCompany.API.DTOs.Destination;

public class DiemDenResponseDto
{
    public int MaDiemDen { get; set; }
    public string TenDiemDen { get; set; } = null!;
    public string? QuocGia { get; set; }
    public string? ThanhPho { get; set; }
    public string? MoTa { get; set; }
}