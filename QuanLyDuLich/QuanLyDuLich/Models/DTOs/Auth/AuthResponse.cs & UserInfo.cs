namespace QuanLyDuLich.Models.DTOs.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public UserInfo User { get; set; }
    }

    public class UserInfo
    {
        public int MaTaiKhoan { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string VaiTro { get; set; }
        public string TrangThai { get; set; }
        public int? MaKhachHang { get; set; }
        public string MaNhanVien { get; set; }
        public string LoaiNhanVien { get; set; }
    }
}