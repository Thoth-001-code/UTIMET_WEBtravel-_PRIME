import { getMyBookings } from '../services/bookingService.js';
import { formatMoney, formatDate } from '../utils.js';
import { apiRequest } from '../api.js';

export async function initMyBookings() {
    const data = await getMyBookings();
    const tableBody = document.querySelector('#my-bookings-table tbody');
    tableBody.innerHTML = data.items.map(b => `
        <tr>
            <td>${b.maCodeDat}</td>
            <td>${formatDate(b.ngayDat)}</td>
            <td>${b.soLuongNguoi}</td>
            <td>${formatMoney(b.tongTien)}</td>
            <td><span class="badge badge-${getStatusClass(b.trangThai)}">${b.trangThai}</span></td>
        </tr>
    `).join('');
}

function getStatusClass(status) {
    if (status === 'hoan_thanh') return 'success';
    if (status === 'da_huy') return 'danger';
    return 'warning';
}
