import { getBookings, updateBookingStatus } from '../services/bookingService.js';
import { formatMoney, formatDate } from '../utils.js';
import { apiRequest } from '../api.js';

let currentPage = 1;

export async function initBookings() {
    await loadBookings();
}

async function loadBookings() {
    const data = await getBookings(currentPage);
    const tbody = document.querySelector('#booking-table tbody');
    tbody.innerHTML = data.items.map(b => `
        <tr>
            <td>${b.maCodeDat}</td>
            <td>${b.tenKhachHang}</td>
            <td>${formatDate(b.ngayDat)}</td>
            <td>${formatMoney(b.tongTien)}</td>
            <td><span class="badge badge-${getStatusClass(b.trangThai)}">${b.trangThai}</span></td>
            <td>
                <select onchange="updateStatus(${b.maDatTour}, this.value)">
                    <option value="cho_xac_nhan" ${b.trangThai === 'cho_xac_nhan' ? 'selected' : ''}>Chờ xác nhận</option>
                    <option value="da_xac_nhan" ${b.trangThai === 'da_xac_nhan' ? 'selected' : ''}>Đã xác nhận</option>
                    <option value="hoan_thanh" ${b.trangThai === 'hoan_thanh' ? 'selected' : ''}>Hoàn thành</option>
                    <option value="da_huy" ${b.trangThai === 'da_huy' ? 'selected' : ''}>Đã hủy</option>
                </select>
            </td>
        </tr>
    `).join('');
    document.getElementById('booking-pagination').innerHTML = `
        <button class="btn btn-primary" ${currentPage <= 1 ? 'disabled' : ''} onclick="changeBookingPage(${currentPage - 1})">Trước</button>
        <span style="padding: 10px;">Trang ${currentPage} / ${data.totalPages}</span>
        <button class="btn btn-primary" ${currentPage >= data.totalPages ? 'disabled' : ''} onclick="changeBookingPage(${currentPage + 1})">Sau</button>
    `;
}

window.updateStatus = async function(id, status) {
    await updateBookingStatus(id, status);
    loadBookings();
};

window.changeBookingPage = function(page) {
    currentPage = page;
    loadBookings();
};

function getStatusClass(status) {
    if (status === 'hoan_thanh') return 'success';
    if (status === 'da_huy') return 'danger';
    return 'warning';
}
