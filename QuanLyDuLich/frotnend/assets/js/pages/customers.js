import { getCustomers } from '../services/customerService.js';

let currentPage = 1;
let searchQuery = '';

export async function initCustomers() {
    await loadCustomers();
    document.getElementById('search-customer').addEventListener('input', (e) => {
        searchQuery = e.target.value;
        currentPage = 1;
        loadCustomers();
    });
}

async function loadCustomers() {
    const data = await getCustomers(searchQuery, currentPage);
    const tbody = document.querySelector('#customer-table tbody');
    tbody.innerHTML = data.items.map(c => `
        <tr>
            <td>${c.maKhachHang}</td>
            <td>${c.hoTen}</td>
            <td>${c.email}</td>
            <td>${c.soDienThoai}</td>
            <td></td>
        </tr>
    `).join('');
    document.getElementById('customer-pagination').innerHTML = `
        <button class="btn btn-primary" ${currentPage <= 1 ? 'disabled' : ''} onclick="changeCustomerPage(${currentPage - 1})">Trước</button>
        <span style="padding: 10px;">Trang ${currentPage} / ${data.totalPages}</span>
        <button class="btn btn-primary" ${currentPage >= data.totalPages ? 'disabled' : ''} onclick="changeCustomerPage(${currentPage + 1})">Sau</button>
    `;
}

window.changeCustomerPage = function(page) {
    currentPage = page;
    loadCustomers();
};
