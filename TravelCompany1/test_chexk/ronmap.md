# Lộ trình nâng cấp dự án TravelCompany API
---

## Tổng quan
Dựa trên kết quả đánh giá, đây là lộ trình từng bước để nâng cấp dự án lên mức production-ready.

---

## Giai đoạn 1: Khắc phục lỗi và cải thiện ngay lập tức (Critical)
| Công việc | Mô tả chi tiết | Ưu tiên | Thời gian ước tính | Phụ thuộc | Tiêu chí hoàn thành |
|------------|-----------------|---------|--------------------|-----------|---------------------|
| 1.1 Thêm Unit Tests cơ bản | Tạo test project với xUnit, Moq, EF Core InMemory và viết unit tests cho các services chính (AuthService, BookingService, TourService) | 🔴 Cao | 8-10 giờ | Không | Test project được chạy pass ≥70% code coverage cho các services chính |
| 1.2 Khắc phục N+1 Query | Sửa ReportService.GetTopCustomersAsync() để tránh N+1 query bằng cách dùng projection hoặc Include đúng cách | 🔴 Cao | 1 giờ | Không | Không có N+1 query, performance được kiểm tra bằng SQL Profiler hoặc EF Core Logs |
| 1.3 Cải thiện Secrets Management (Dev) | Di chuyển JWT key và connection string ra khỏi appsettings.json vào User Secrets cho môi trường Development | 🔴 Cao | 1 giờ | Không | Dự án chạy bình thường với User Secrets, không có secret nào trong appsettings.json |
| 1.4 Thêm Validators cho các DTO còn thiếu | Tạo FluentValidation validators cho: TourCreateDto, TourUpdateDto, DiemDenCreateDto, DiemDenUpdateDto, ThanhToanCreateDto, CustomerRegisterDto,... | 🟡 Trung bình | 4-5 giờ | Không | Tất cả DTO đều có validator, validation được gọi thủ công trong controllers/services |

---

## Giai đoạn 2: Cải thiện bảo mật và chất lượng (Enhancements)
| Công việc | Mô tả chi tiết | Ưu tiên | Thời gian ước tính | Phụ thuộc | Tiêu chí hoàn thành |
|------------|-----------------|---------|--------------------|-----------|---------------------|
| 2.1 Cấu hình CORS chặt chẽ hơn | Thay đổi CORS policy từ AllowAll thành policy giới hạn origins, methods, headers cho production | 🔴 Cao | 1 giờ | Không | CORS chỉ cho phép các origins được chỉ định, methods GET/POST/PUT/DELETE cần thiết |
| 2.2 Sử dụng Exception chuyên biệt | Tạo các exception class riêng (NotFoundException, ValidationException, UnauthorizedException,...) và thay thế throw new Exception() | 🟡 Trung bình | 3-4 giờ | Không | Không còn throw new Exception() nào trong toàn bộ dự án |
| 2.3 Thêm API Versioning | Thêm API versioning bằng Microsoft.AspNetCore.Mvc.Versioning | 🟡 Trung bình | 2 giờ | Không | API hỗ trợ versioning, các endpoint được đánh version (ex: /api/v1/tours |
| 2.4 Thêm Health Checks | Thêm health check endpoint cho database và dịch vụ | 🟡 Trung bình | 1 giờ | Không | Có endpoint /health trả về trạng thái health của hệ thống |

---

## Giai đoạn 3: Tính năng nâng cao (Advanced)
| Công việc | Mô tả chi tiết | Ưu tiên | Thời gian ước tính | Phụ thuộc | Tiêu chí hoàn thành |
|------------|-----------------|---------|--------------------|-----------|---------------------|
| 3.1 Thêm Rate Limiting | Thêm rate limiting để chống DDoS và quá nhiều request | 🟡 Trung bình | 2-3 giờ | Không | Rate limiting hoạt động, giới hạn số request/phút theo IP hoặc user |
| 3.2 Thêm Integration Tests | Viết integration tests cho các API endpoint chính | 🟡 Trung bình | 6-8 giờ | 1.1 | Integration tests pass cho các endpoint chính |
| 3.3 Cải thiện Secrets Management (Prod) | Thiết lập cách quản lý secret cho production (Azure Key Vault / AWS Secrets Manager) | 🔴 Cao | 2-3 giờ | 1.3 | Secrets được quản lý bằng cloud secret manager, không có secret nào trong file cấu hình |
| 3.4 Thêm Logging nâng cao | Cấu hình Serilog gửi log đến centralized logging system (Seq, ELK,...) | 🟢 Thấp | 3-4 giờ | Không | Log được gửi đến centralized system, có thể xem/search dễ dàng |
| 3.5 Cải thiện Performance | Thêm caching (Redis), optimize queries, thêm pagination cho tất cả các listing endpoint | 🟢 Thấp | 8-10 giờ | Không | Tất cả listing endpoint đều có pagination, performance được cải thiện ≥20% |

---

## Tổng thời gian ước tính
- Giai đoạn 1: **14-17 giờ**
- Giai đoạn 2: **7-8 giờ**
- Giai đoạn 3: **21-28 giờ**
- **Tổng cộng: ~42-53 giờ (~1-1.5 tuần làm full-time)

---

## Lưu ý
- Ưu tiên hoàn thành Giai đoạn 1 trước vì các vấn đề critical
- Mỗi công việc nên được tạo thành 1 issue/ticket riêng
- Thực hiện CI/CD để tự động chạy tests khi có thay đổi
