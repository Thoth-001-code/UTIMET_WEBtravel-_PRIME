using Microsoft.AspNetCore.Http;

namespace TravelCompany.API.DTOs.Tour;

public class AnhTourUploadDto
{
    public int MaTour { get; set; }
    public IFormFile File { get; set; } = null!;
    public string? MoTaAnh { get; set; }
    public int ThuTu { get; set; } = 0;
}
