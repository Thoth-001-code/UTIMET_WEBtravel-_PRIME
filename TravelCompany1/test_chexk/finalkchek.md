# Project Evaluation Prompt for AI

You are an expert .NET backend architect. I have built a complete backend API for a travel company management system using .NET 8 Web API, Entity Framework Core (Code First), JWT authentication, AutoMapper, FluentValidation (manual), Serilog, and EPPlus.

Please analyze my entire project structure and code files, then provide a detailed evaluation covering:

## 1. Project Overview
- List all major components (Controllers, Services, Data, DTOs, Entities, Helpers, Middlewares, Validators, Mappings).
- Identify the purpose of each component.

## 2. Architecture & Design
- Does it follow 3-layer / N-tier architecture (Controller → Service → Data)?
- Any code smells or anti-patterns?
- Is dependency injection used correctly?
- Any circular dependencies?

## 3. Code Quality
- Naming conventions, readability, comments.
- Use of async/await throughout.
- Error handling (try-catch, global exception middleware).
- Logging (Serilog) – properly configured?
- Validation (FluentValidation – manual calls in services) – all DTOs validated?
- AutoMapper usage – profiles correctly defined and mappings complete?

## 4. Database & Entity Framework
- Entities correctly mapped with Fluent API? (keys, relationships, indexes, check constraints)
- Migrations properly set up? (InitialCreate, AddPasswordToKhachHang)
- Any N+1 query risks? (check Includes, AsNoTracking usage)
- Indexes added for performance?

## 5. Authentication & Authorization
- JWT token generation and validation – secure?
- Role-based authorization (quan_tri, quan_ly, nhan_vien, ke_toan, customer) – endpoints properly protected with `[Authorize]`?
- Password hashing (BCrypt) correctly implemented?

## 6. Feature Completion (Phases 1-7)
Check if all features from the roadmap are present:

- **Phase 1-2:** Project setup, Entities, DbContext, JWT, Auth for internal users.
- **Phase 2b:** Customer registration/login with `KhachHang` table, `MatKhau` column.
- **Phase 3:** CRUD for DiemDen, Tour, LichKhoiHanh, ChiTietLichTrinh (public GET, protected POST/PUT/DELETE).
- **Phase 4:** Booking creation by customer/staff, confirmation, cancellation, listing with pagination and filtering, booking history.
- **Phase 5:** Payment recording, invoice generation, balance tracking.
- **Phase 6:** Reporting (revenue by tour/month, top customers, booking status), Excel export.
- **Phase 7:** Global exception middleware, Serilog logging, FluentValidation manual usage, performance optimizations (AsNoTracking, indexes), unit test examples.

## 7. Security & Best Practices
- Secrets (JWT key, connection string) stored in `appsettings.json` (should be user secrets for dev)?
- HTTPS enforced? (UseHttpsRedirection)
- CORS configured properly?
- SQL injection risks mitigated?
- Hardcoded sensitive information?

## 8. Missing Parts or Bugs
- Identify missing endpoints, incomplete logic, or potential bugs.
- Suggest improvements or missing validations.

## 9. Testing
- Unit tests present? (xUnit, Moq, InMemory)
- Integration tests? If not, recommend.

## 10. Final Verdict
- What works well?
- What needs immediate fixing?
- Overall rating (1-10) and summary.

Please scan all files in the provided project. If you need me to paste specific files, ask.