using Microsoft.AspNetCore.Http;

namespace TravelCompany.API.DTOs.Tour;

public class AnhTourMultipleUploadDto
{
    public int MaTour { get; set; }
    public List<IFormFile> Files { get; set; } = new List<IFormFile>();
}
