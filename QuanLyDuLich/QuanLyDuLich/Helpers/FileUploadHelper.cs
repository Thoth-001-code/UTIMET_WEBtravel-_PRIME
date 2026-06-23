using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace QuanLyDuLich.Helpers
{
    public static class FileUploadHelper
    {
        public static async Task<string> SaveFileAsync(IFormFile file, string folder = "tours")
        {
            if (file == null || file.Length == 0)
                return null;

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var folderPath = Path.Combine("wwwroot", "images", folder);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/images/{folder}/{fileName}";
        }

        public static void DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            var physicalPath = Path.Combine("wwwroot", filePath.TrimStart('/'));
            if (File.Exists(physicalPath))
                File.Delete(physicalPath);
        }
    }
}