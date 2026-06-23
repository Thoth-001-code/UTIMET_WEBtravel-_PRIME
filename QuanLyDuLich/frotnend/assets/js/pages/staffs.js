import { apiRequest } from '../api.js';

export async function initStaffs() {
    await loadStaffs();
}

async function loadStaffs() {
    const data = await apiRequest('/staffs');
    const tbody = document.querySelector('#staff-table tbody');
    tbody.innerHTML = data.items.map(s => `
        <tr>
            <td>${s.maNhanVien}</td>
            <td>${s.hoTen}</td>
            <td>${s.chucVu}</td>
            <td>${s.soDienThoai}</td>
            <td></td>
        </tr>
    `).join('');
}
