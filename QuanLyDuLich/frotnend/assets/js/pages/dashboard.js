import { apiRequest } from '../api.js';
import { formatDate, formatMoney } from '../utils.js';

export async function initDashboard() {
    const stats = await apiRequest('/dashboard/stats');
    const recentBookings = await apiRequest('/dashboard/recent-bookings');

    const statsContainer = document.getElementById('stats');
    statsContainer.innerHTML = `
        <div class="card">
            <h3>Tổng Tour</h3>
            <p style="font-size: 24px; font-weight: bold; color: #667eea;">${stats.tongTour}</p>
        </div>
        <div class="card">
            <h3>Tổng Đặt</h3>
            <p style="font-size: 24px; font-weight: bold; color: #27ae60;">${stats.tongDat}</p>
        </div>
        <div class="card">
            <h3>Tổng Doanh thu</h3>
            <p style="font-size: 24px; font-weight: bold; color: #f39c12;">${formatMoney(stats.tongDoanhThu)}</p>
        </div>
        <div class="card">
            <h3>Tổng Khách hàng</h3>
            <p style="font-size: 24px; font-weight: bold; color: #e74c3c;">${stats.tongKhachHang}</p>
        </div>
    `;

    const tableBody = document.querySelector('#recent-bookings tbody');
    tableBody.innerHTML = recentBookings.map(b => `
        <tr>
            <td>${b.maCodeDat}</td>
            <td>${b.tenKhachHang}</td>
            <td>${formatDate(b.ngayDat)}</td>
            <td><span class="badge badge-${getStatusClass(b.trangThai)}">${b.trangThai}</span></td>
        </tr>
    `).join('');
}

function getStatusClass(status) {
    if (status === 'hoan_thanh') return 'success';
    if (status === 'da_huy') return 'danger';
    return 'warning';
}
