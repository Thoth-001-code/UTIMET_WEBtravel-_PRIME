import { apiRequest } from '../api.js';

export async function initDestinations() {
    await loadDestinations();
}

async function loadDestinations() {
    const data = await apiRequest('/diemden');
    const tbody = document.querySelector('#destination-table tbody');
    tbody.innerHTML = data.map(d => `
        <tr>
            <td>${d.maDiemDen}</td>
            <td>${d.tenDiemDen}</td>
            <td>${d.moTa || ''}</td>
            <td></td>
        </tr>
    `).join('');
}
