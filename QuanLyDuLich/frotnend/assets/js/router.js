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
        app.innerHTML = await fetch(`pages/auth/${path}.html`).then(r => r.text());
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
        <div id="header">
            <div class="d-flex align-items-center gap-2">
                <h3>Quản lý Du lịch</h3>
            </div>
            <div class="d-flex align-items-center gap-2">
                <span>Xin chào, ${user?.hoTen}</span>
                <button class="btn btn-warning" onclick="logoutAndNavigate()">Đăng xuất</button>
            </div>
        </div>
        <div id="sidebar">
            <ul>
                <li><a href="#" onclick="navigate('dashboard')">Dashboard</a></li>
                <li><a href="#" onclick="navigate('tours')">Quản lý Tour</a></li>
                <li><a href="#" onclick="navigate('destinations')">Điểm đến</a></li>
                <li><a href="#" onclick="navigate('schedules')">Lịch khởi hành</a></li>
                <li><a href="#" onclick="navigate('bookings')">Đặt tour</a></li>
                <li><a href="#" onclick="navigate('customers')">Khách hàng</a></li>
                <li><a href="#" onclick="navigate('staffs')">Nhân viên</a></li>
            </ul>
        </div>
        <div id="main-content"></div>
    `;
    const mainContent = document.getElementById('main-content');
    const html = await fetch(`pages/admin/${page}.html`).then(r => r.text());
    mainContent.innerHTML = html;
}

async function renderCustomerLayout(page) {
    const user = await getCurrentUser();
    document.getElementById('app').innerHTML = `
        <div id="header">
            <div class="d-flex align-items-center gap-2">
                <h3>Du lịch</h3>
            </div>
            <div class="d-flex align-items-center gap-2">
                <a href="#" onclick="navigate('home')" style="color: white; margin-right: 20px;">Trang chủ</a>
                ${isAuthenticated() ? `
                    <a href="#" onclick="navigate('my-bookings')" style="color: white; margin-right: 20px;">Đặt tour của tôi</a>
                    <span style="margin-right: 10px;">Xin chào, ${user?.hoTen}</span>
                    <button class="btn btn-warning" onclick="logoutAndNavigate()">Đăng xuất</button>
                ` : `
                    <button class="btn btn-primary" onclick="navigate('login')">Đăng nhập</button>
                `}
            </div>
        </div>
        <div style="padding: 30px; margin-top: 0;" id="main-content"></div>
    `;
    const mainContent = document.getElementById('main-content');
    const htmlFile = page === 'home' ? 'home.html' : page === 'tour-detail' ? 'tour-detail.html' : page === 'booking' ? 'booking.html' : 'my-bookings.html';
    const html = await fetch(`pages/customer/${htmlFile}`).then(r => r.text());
    mainContent.innerHTML = html;
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
