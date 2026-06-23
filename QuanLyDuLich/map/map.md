frontend/
├── index.html                 # Trang chính (chứa #app, #sidebar, #header)
├── config.js                  # BASE_URL
├── assets/
│   ├── css/
│   │   └── style.css          # Gộp tất cả CSS (ko tách nhỏ)
│   ├── js/
│   │   ├── app.js             # Khởi tạo, gọi router
│   │   ├── api.js             # Hàm gọi API (fetch + token)
│   │   ├── auth.js            # login, register, logout, getCurrentUser
│   │   ├── router.js          # Định nghĩa route + render trang
│   │   ├── utils.js           # formatDate, formatMoney, validate...
│   │   ├── services/          # Gọi API cụ thể
│   │   │   ├── tourService.js
│   │   │   ├── bookingService.js
│   │   │   └── customerService.js
│   │   └── pages/             # Logic cho từng trang
│   │       ├── dashboard.js
│   │       ├── tours.js
│   │       ├── destinations.js
│   │       ├── schedules.js
│   │       ├── bookings.js
│   │       ├── customers.js
│   │       ├── staffs.js
│   │       ├── home.js
│   │       ├── tourDetail.js
│   │       ├── bookingForm.js
│   │       └── myBookings.js
│   └── images/
└── pages/                     # Nội dung HTML các trang (sẽ fetch về)
    ├── auth/
    │   ├── login.html
    │   └── register.html
    ├── admin/
    │   ├── dashboard.html
    │   ├── tours.html
    │   ├── tour-edit.html
    │   ├── destinations.html
    │   ├── schedules.html
    │   ├── bookings.html
    │   ├── customers.html
    │   └── staffs.html
    └── customer/
        ├── home.html
        ├── tour-detail.html
        ├── booking.html
        └── my-bookings.html