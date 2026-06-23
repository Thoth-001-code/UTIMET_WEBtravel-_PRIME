import { apiRequest } from '../api.js';
import { formatDate } from '../utils.js';

let currentPage = 1;

export async function initSchedules() {
    await loadSchedules();
}

async function loadSchedules() {
    const data = await apiRequest(`/schedules?page=${currentPage}&pageSize=10`);
    const tbody = document.querySelector('#schedule-table tbody');
    tbody.innerHTML = data.items.map(s => `
        <tr>
            <td>${s.maLich}</td>
            <td>${s.tenTour}</td>
            <td>${formatDate(s.ngayKhoiHanh)}</td>
            <td>${s.soChoConLai}</td>
            <td></td>
        </tr>
    `).join('');
}
