import { getCurrentUser, isAuthenticated, isAdmin } from './auth.js';

let routes = {};
let currentRoute = '';

function registerRoute(path, handler) {
    routes[path] = handler;
}

async function navigate(path, params = {}) {
    currentRoute = path;
    window.history.pushState({ path, params }, '', `#${path}`);
    await render(path, params);
}

async function render(path, params = {}) {
  const app = document.getElementById('app');
  const user = await getCurrentUser();

  if (!routes[path]) {
    path = 'home';
  }

  if (['login', 'register'].includes(path)) {
    if (isAuthenticated()) {
      if (isAdmin()) {
        navigate('dashboard');
      } else {
        navigate('home');
      }
      return;
    }
  } else if (['dashboard', 'tours', 'destinations', 'schedules', 'bookings', 'customers', 'staffs', 'tour-edit'].includes(path)) {
    if (!isAuthenticated() || !isAdmin()) {
      navigate('login');
      return;
    }
  } else if (['tour-detail', 'booking', 'my-bookings'].includes(path)) {
    if (!isAuthenticated()) {
      navigate('login');
      return;
    }
  }

  if (['login', 'register'].includes(path)) {
    // Render auth page with frontendzzz design
    app.innerHTML = `
      <div class="container" style="padding: 20px;">
        <div class="card" style="max-width: 500px; margin: 40px auto; padding: 40px 32px; border-radius: 32px; box-shadow: 0 25px 50px -12px rgba(0,0,0,0.25);">
          <button class="back-home-btn" id="backToHomeBtn" style="background: white; border: 1px solid #1e5a88; color: #1e5a88; font-size: 0.85rem; cursor: pointer; display: inline-flex; align-items: center; gap: 6px; padding: 8px 18px; border-radius: 40px; margin-bottom: 20px;">← Quay lại trang chủ</button>
          <div class="header" style="text-align: center; margin-bottom: 32px;">
            <h1 id="mainTitle" style="font-size: 1.8rem; color: #1e5a88; margin-bottom: 8px;">${path === 'login' ? 'Đăng nhập' : 'Đăng ký'}</h1>
            <p id="subTitle" style="color: #5a7d9a; font-size: 0.9rem;">${path === 'login' ? 'Đăng nhập vào hệ thống quản lý du lịch' : 'Tạo tài khoản để trải nghiệm dịch vụ'}</p>
          </div>
          <div class="tabs" style="display: flex; gap: 16px; margin-bottom: 32px; border-bottom: 2px solid #e2e8f0;">
            <button class="tab-btn ${path === 'login' ? 'active' : ''}" data-tab="login" style="flex:1; background:none; border:none; padding:12px 0; font-size:1rem; font-weight:700; cursor:pointer; color:#8aaec0; position:relative;">Đăng nhập</button>
            <button class="tab-btn ${path === 'register' ? 'active' : ''}" data-tab="register" style="flex:1; background:none; border:none; padding:12px 0; font-size:1rem; font-weight:700; cursor:pointer; color:#8aaec0; position:relative;">Đăng ký</button>
          </div>
          <div id="loginPane" class="form-pane ${path === 'login' ? 'active' : ''}" style="display: ${path === 'login' ? 'block' : 'none'}">
            <form id="login-form">
              <div class="form-group" style="margin-bottom: 20px;">
                <label style="font-weight:600; display:block; margin-bottom:8px; color:#2a5f8a; font-size:0.9rem;">Email</label>
                <input type="email" id="email" class="form-control" placeholder="example@email.com" style="width:100%; padding:12px 16px; border:1px solid #c5d9e8; border-radius:60px; font-size:1rem; outline:none;">
              </div>
              <div class="form-group" style="margin-bottom: 20px;">
                <label style="font-weight:600; display:block; margin-bottom:8px; color:#2a5f8a; font-size:0.9rem;">Mật khẩu</label>
                <div class="password-wrapper" style="position:relative; display:flex; align-items:center;">
                  <input type="password" id="password" class="form-control" placeholder="Nhập mật khẩu" style="width:100%; padding:12px 16px; border:1px solid #c5d9e8; border-radius:60px; font-size:1rem; outline:none; flex:1; padding-right:50px;">
                  <button type="button" class="toggle-password" data-target="password" style="position:absolute; right:16px; background:none; border:none; cursor:pointer; font-size:1.2rem; color:#8aaec0;">👁️</button>
                </div>
              </div>
              <button type="submit" class="btn-submit" style="width:100%; background:linear-gradient(135deg, #1e5a88, #144d74); color:white; border:none; border-radius:60px; padding:14px 24px; font-size:1rem; font-weight:700; cursor:pointer;">Đăng nhập</button>
            </form>
          </div>
          <div id="registerPane" class="form-pane ${path === 'register' ? 'active' : ''}" style="display: ${path === 'register' ? 'block' : 'none'}">
            <form id="register-form">
              <div class="form-group" style="margin-bottom: 20px;">
                <label style="font-weight:600; display:block; margin-bottom:8px; color:#2a5f8a; font-size:0.9rem;">Họ tên</label>
                <input type="text" id="fullName" class="form-control" placeholder="Nhập họ tên đầy đủ" style="width:100%; padding:12px 16px; border:1px solid #c5d9e8; border-radius:60px; font-size:1rem; outline:none;">
              </div>
              <div class="form-group" style="margin-bottom: 20px;">
                <label style="font-weight:600; display:block; margin-bottom:8px; color:#2a5f8a; font-size:0.9rem;">Email</label>
                <input type="email" id="email" class="form-control" placeholder="example@email.com" style="width:100%; padding:12px 16px; border:1px solid #c5d9e8; border-radius:60px; font-size:1rem; outline:none;">
              </div>
              <div class="form-group" style="margin-bottom: 20px;">
                <label style="font-weight:600; display:block; margin-bottom:8px; color:#2a5f8a; font-size:0.9rem;">Số điện thoại</label>
                <input type="tel" id="phone" class="form-control" placeholder="0912345678" style="width:100%; padding:12px 16px; border:1px solid #c5d9e8; border-radius:60px; font-size:1rem; outline:none;">
              </div>
              <div class="form-group" style="margin-bottom: 20px;">
                <label style="font-weight:600; display:block; margin-bottom:8px; color:#2a5f8a; font-size:0.9rem;">Mật khẩu</label>
                <div class="password-wrapper" style="position:relative; display:flex; align-items:center;">
                  <input type="password" id="password" class="form-control" placeholder="Nhập mật khẩu" style="width:100%; padding:12px 16px; border:1px solid #c5d9e8; border-radius:60px; font-size:1rem; outline:none; flex:1; padding-right:50px;">
                  <button type="button" class="toggle-password" data-target="password" style="position:absolute; right:16px; background:none; border:none; cursor:pointer; font-size:1.2rem; color:#8aaec0;">👁️</button>
                </div>
              </div>
              <button type="submit" class="btn-submit" style="width:100%; background:linear-gradient(135deg, #1e5a88, #144d74); color:white; border:none; border-radius:60px; padding:14px 24px; font-size:1rem; font-weight:700; cursor:pointer;">Đăng ký</button>
            </form>
          </div>
        </div>
      </div>
      <style>
        body { background: linear-gradient(135deg, #e8f4fd 0%, #d9eefb 100%); font-family: system-ui, "Segoe UI", "Inter", "Helvetica Neue", sans-serif; }
        .tab-btn.active { color: #1e5a88; }
        .tab-btn.active::after { content: ""; position: absolute; bottom: -2px; left: 0; width:100%; height:3px; background: #5aa9dd; border-radius:3px; }
      </style>
    `;
    
    // Add tab switching logic
    setTimeout(() => {
      document.querySelectorAll('.tab-btn').forEach(btn => {
        btn.addEventListener('click', () => {
          const tab = btn.getAttribute('data-tab');
          navigate(tab);
        });
      });
      
      document.getElementById('backToHomeBtn')?.addEventListener('click', () => {
        navigate('home');
      });
      
      document.querySelectorAll('.toggle-password').forEach(btn => {
        btn.addEventListener('click', () => {
          const targetId = btn.getAttribute('data-target');
          const input = document.getElementById(targetId);
          if (input) {
            input.type = input.type === 'password' ? 'text' : 'password';
            btn.textContent = input.type === 'password' ? '👁️' : '🙈';
          }
        });
      });
    }, 0);
    
  } else if (['dashboard', 'tours', 'destinations', 'schedules', 'bookings', 'customers', 'staffs', 'tour-edit'].includes(path)) {
    await renderAdminLayout(path);
  } else {
    await renderCustomerLayout(path);
  }

  if (routes[path]) {
    await routes[path](params);
  }
}

async function renderAdminLayout(page) {
    const user = await getCurrentUser();
    document.getElementById('app').innerHTML = `
        <header class="header" style="background: linear-gradient(135deg, #1a472a, #0e2f1c);">
            <div class="container header-container">
                <div class="logo">
                    <span class="logo-icon">🏝️</span>
                    <span class="logo-text">TourifyVN - Admin</span>
                </div>
                <div class="header-actions">
                    <span style="color:white;">Xin chào, ${user?.hoTen}</span>
                    <button class="btn-logout" id="logoutBtn" style="background:#a13e3e;">🚪 Đăng xuất</button>
                </div>
            </div>
        </header>
        <div style="display: flex;">
            <div id="sidebar" style="width: 250px; min-height: calc(100vh - 80px); background: #0e2f1c; padding: 20px;">
                <ul style="list-style: none; padding: 0;">
                    <li><a href="#" onclick="navigate('dashboard'); return false;" style="display:block; color: #c8dcc0; text-decoration: none; padding:12px 16px; border-radius:12px; margin-bottom:8px;">📊 Dashboard</a></li>
                    <li><a href="#" onclick="navigate('tours'); return false;" style="display:block; color: #c8dcc0; text-decoration: none; padding:12px 16px; border-radius:12px; margin-bottom:8px;">🧳 Quản lý Tour</a></li>
                    <li><a href="#" onclick="navigate('destinations'); return false;" style="display:block; color: #c8dcc0; text-decoration: none; padding:12px 16px; border-radius:12px; margin-bottom:8px;">📍 Điểm đến</a></li>
                    <li><a href="#" onclick="navigate('schedules'); return false;" style="display:block; color: #c8dcc0; text-decoration: none; padding:12px 16px; border-radius:12px; margin-bottom:8px;">📅 Lịch khởi hành</a></li>
                    <li><a href="#" onclick="navigate('bookings'); return false;" style="display:block; color: #c8dcc0; text-decoration: none; padding:12px 16px; border-radius:12px; margin-bottom:8px;">📝 Đặt tour</a></li>
                    <li><a href="#" onclick="navigate('customers'); return false;" style="display:block; color: #c8dcc0; text-decoration: none; padding:12px 16px; border-radius:12px; margin-bottom:8px;">👤 Khách hàng</a></li>
                    <li><a href="#" onclick="navigate('staffs'); return false;" style="display:block; color: #c8dcc0; text-decoration: none; padding:12px 16px; border-radius:12px; margin-bottom:8px;">👥 Nhân viên</a></li>
                </ul>
            </div>
            <div id="main-content" style="flex:1; padding:30px;"></div>
        </div>
    `;
    document.getElementById('logoutBtn')?.addEventListener('click', () => logoutAndNavigate());
    const mainContent = document.getElementById('main-content');
    const html = await fetch(`pages/admin/${page}.html`).then(r => r.text());
    mainContent.innerHTML = html;
}

async function renderCustomerLayout(page) {
  const user = await getCurrentUser();
  document.getElementById('app').innerHTML = `
    <header class="header">
      <div class="container header-container">
        <div class="logo">
          <span class="logo-icon">🏝️</span>
          <span class="logo-text">TourifyVN</span>
        </div>
        ${isAuthenticated() ? `
          <div class="header-actions">
            <button class="btn-season" id="seasonBtn">🌾 Tour mùa</button>
            <button class="btn-me" id="myAccountBtn">👤 Tôi</button>
            <button class="btn-logout" id="logoutBtn">🚪 Đăng xuất</button>
          </div>
        ` : `
          <div class="auth-buttons">
            <button class="btn-login" id="loginBtn">Đăng nhập</button>
            <button class="btn-register" id="registerBtn">Đăng ký</button>
          </div>
        `}
      </div>
    </header>
    <div id="main-content"></div>
  `;
  const mainContent = document.getElementById('main-content');
  
  // Add event listeners after rendering
  setTimeout(() => {
    if (isAuthenticated()) {
      document.getElementById('logoutBtn')?.addEventListener('click', () => {
        logoutAndNavigate();
      });
      document.getElementById('myAccountBtn')?.addEventListener('click', () => {
        navigate('my-bookings');
      });
      document.getElementById('seasonBtn')?.addEventListener('click', () => {
        // Add seasonal tours page later
      });
    } else {
      document.getElementById('loginBtn')?.addEventListener('click', () => {
        navigate('login');
      });
      document.getElementById('registerBtn')?.addEventListener('click', () => {
        navigate('register');
      });
    }
  }, 0);
  
  // Only load HTML file for pages that don't need custom rendering from their init functions
  if (['home', 'booking', 'my-bookings'].includes(page)) {
    const htmlFile = page === 'home' ? 'home.html' : page === 'booking' ? 'booking.html' : 'my-bookings.html';
    const html = await fetch(`pages/customer/${htmlFile}`).then(r => r.text());
    mainContent.innerHTML = html;
  }
}

function logoutAndNavigate() {
    import('./auth.js').then(module => {
        module.logout();
        navigate('login');
    });
}

window.navigate = navigate;
window.logoutAndNavigate = logoutAndNavigate;

export { registerRoute, navigate, render };
