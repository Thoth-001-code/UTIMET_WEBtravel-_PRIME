import { getMyBookings } from '../services/bookingService.js';
import { formatMoney, formatDate } from '../utils.js';
import { apiRequest } from '../api.js';

export async function initMyBookings() {
    let data = { items: [] };
    try {
        data = await getMyBookings();
        // Handle both cases: if data is array directly or has items property
        if (Array.isArray(data)) {
            data = { items: data };
        } else if (!data.items) {
            data.items = [];
        }
    } catch (e) {
        console.error('Error loading bookings:', e);
        alert('Lỗi khi tải danh sách đặt tour');
    }

    const tbody = document.querySelector('#my-bookings-table tbody');
    if (data.items.length === 0) {
        tbody.innerHTML = `
            <tr>
                <td colspan="5" style="text-align: center; padding: 40px; color: #666;">
                    Chưa có đặt tour nào
                </td>
            </tr>
        `;
        return;
    }

    tbody.innerHTML = data.items.map(b => `
        <tr style="border-bottom: 1px solid #eee;">
            <td style="padding: 12px;">${b.maCodeDat}</td>
            <td style="padding: 12px;">${formatDate(b.ngayKhoiHanh)}</td>
            <td style="padding: 12px;">${b.soLuongNguoi}</td>
            <td style="padding: 12px;">${formatMoney(b.tongTien)}</td>
            <td style="padding: 12px;">
                <span style="display: inline-block; padding: 4px 12px; border-radius: 20px; font-size: 0.8rem; font-weight: 600; background: ${getStatusBackground(b.trangThai)}; color: ${getStatusTextColor(b.trangThai)};">
                    ${getStatusLabel(b.trangThai)}
                </span>
            </td>
        </tr>
        `).join('');
}

function getStatusLabel(status) {
    const labels = {
        'cho_xac_nhan': 'Chờ xác nhận',
        'da_xac_nhan': 'Đã xác nhận',
        'hoan_thanh': 'Hoàn thành',
        'da_huy': 'Đã hủy'
    };
    return labels[status] || status;
}

function getStatusBackground(status) {
    const colors = {
        'cho_xac_nhan': '#fff3cd',
        'da_xac_nhan': '#cce5ff',
        'hoan_thanh': '#d4edda',
        'da_huy': '#f8d7da'
    };
    return colors[status] || '#ddd';
}

function getStatusTextColor(status) {
    const colors = {
        'cho_xac_nhan': '#856404',
        'da_xac_nhan': '#004085',
        'hoan_thanh': '#155724',
        'da_huy': '#721c24'
    };
    return colors[status] || '#333';
}
