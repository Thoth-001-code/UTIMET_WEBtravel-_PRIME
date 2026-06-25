import { apiRequest } from '../api.js';
import { formatDate, formatMoney } from '../utils.js';

export async function initDashboard() {
    let stats = {};
    let recentBookings = [];
    
    try {
        stats = await apiRequest('/dashboard/stats');
        recentBookings = await apiRequest('/dashboard/recent-bookings');
    } catch (e) {
        // Fallback fake data if API isn't available
        stats = { tongTour: 10, tongDat: 50, tongDoanhThu: 50000000, tongKhachHang: 100 };
        recentBookings = [];
    }

    const statsContainer = document.getElementById('stats');
    statsContainer.innerHTML = `
        <div style="background: white; border-radius: 20px; padding: 24px; box-shadow: 0 4px 12px rgba(0,0,0,0.05); text-align: center;">
            <h3 style="color: #666; font-size: 1rem; margin-bottom: 10px;">🧳 Tổng Tour</h3>
            <p style="font-size: 2rem; font-weight: bold; color: #667eea;">${stats.tongTour || 0}</p>
        </div>
        <div style="background: white; border-radius: 20px; padding: 24px; box-shadow: 0 4px 12px rgba(0,0,0,0.05); text-align: center;">
            <h3 style="color: #666; font-size: 1rem; margin-bottom: 10px;">📝 Tổng Đặt</h3>
            <p style="font-size: 2rem; font-weight: bold; color: #27ae60;">${stats.tongDat || 0}</p>
        </div>
        <div style="background: white; border-radius: 20px; padding: 24px; box-shadow: 0 4px 12px rgba(0,0,0,0.05); text-align: center;">
            <h3 style="color: #666; font-size: 1rem; margin-bottom: 10px;">💰 Tổng Doanh thu</h3>
            <p style="font-size: 2rem; font-weight: bold; color: #f39c12;">${formatMoney(stats.tongDoanhThu || 0)}</p>
        </div>
        <div style="background: white; border-radius: 20px; padding: 24px; box-shadow: 0 4px 12px rgba(0,0,0,0.05); text-align: center;">
            <h3 style="color: #666; font-size: 1rem; margin-bottom: 10px;">👥 Tổng Khách hàng</h3>
            <p style="font-size: 2rem; font-weight: bold; color: #e74c3c;">${stats.tongKhachHang || 0}</p>
        </div>
    `;

    const tableBody = document.querySelector('#recent-bookings tbody');
    tableBody.innerHTML = recentBookings.map(b => `
        <tr style="border-bottom: 1px solid #eee;">
            <td style="padding: 12px;">${b.maCodeDat || b.maDat || 'N/A'}</td>
            <td style="padding: 12px;">${b.tenKhachHang || b.hoTen || 'N/A'}</td>
            <td style="padding: 12px;">${b.tenTour || 'N/A'}</td>
            <td style="padding: 12px;">${formatDate(b.ngayDat)}</td>
            <td style="padding: 12px;"><span style="display: inline-block; padding: 4px 12px; border-radius: 20px; font-size: 0.8rem; font-weight: 600; background: ${getStatusColor(b.trangThai)}; color: ${getStatusTextColor(b.trangThai)};">${b.trangThai}</span></td>
        </tr>
    `).join('');
}

function getStatusColor(status) {
    if (status === 'hoan_thanh' || status === 'Hoàn thành') return '#d4edda';
    if (status === 'da_huy' || status === 'Đã hủy') return '#f8d7da';
    return '#fff3cd';
}

function getStatusTextColor(status) {
    if (status === 'hoan_thanh' || status === 'Hoàn thành') return '#155724';
    if (status === 'da_huy' || status === 'Đã hủy') return '#721c24';
    return '#856404';
}
