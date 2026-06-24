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
    let data = { items: [], totalPages: 1 };
    try {
        data = await getCustomers(searchQuery, currentPage);
    } catch (e) {
        // Fallback fake data
        data = { items: [], totalPages: 1 };
    }
    
    const tbody = document.querySelector('#customer-table tbody');
    tbody.innerHTML = data.items.map(c => `
        <tr style="border-bottom: 1px solid #eee;">
            <td style="padding: 12px;">${c.maKhachHang}</td>
            <td style="padding: 12px;">${c.hoTen}</td>
            <td style="padding: 12px;">${c.email}</td>
            <td style="padding: 12px;">${c.soDienThoai || 'N/A'}</td>
            <td style="padding: 12px;">${c.diaChi || 'N/A'}</td>
        </tr>
    `).join('');
    
    const paginationContainer = document.getElementById('customer-pagination');
    paginationContainer.innerHTML = `
        <button ${currentPage <= 1 ? 'disabled' : ''} onclick="changeCustomerPage(${currentPage - 1})" style="background: white; border: 1px solid #ddd; padding: 10px 20px; border-radius: 20px; cursor: pointer; ${currentPage <=1 ? 'opacity:0.5; cursor:not-allowed' : ''};">← Trước</button>
        <span style="padding: 10px; color: #1a472a; font-weight: 600;">Trang ${currentPage} / ${data.totalPages}</span>
        <button ${currentPage >= data.totalPages ? 'disabled' : ''} onclick="changeCustomerPage(${currentPage + 1})" style="background: white; border: 1px solid #ddd; padding: 10px 20px; border-radius: 20px; cursor: pointer; ${currentPage >= data.totalPages ? 'opacity:0.5; cursor:not-allowed' : ''};">Sau →</button>
    `;
}

window.changeCustomerPage = function(page) {
    currentPage = page;
    loadCustomers();
};
