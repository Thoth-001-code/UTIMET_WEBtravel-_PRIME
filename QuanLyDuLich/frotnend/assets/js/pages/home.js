import { getTours } from '../services/tourService.js';
import { formatMoney } from '../utils.js';
import { navigate } from '../router.js';

let currentPage = 1;
let searchQuery = '';

export async function initHome() {
    await loadTours();
    const searchInput = document.getElementById('search-tour-customer');
    if (searchInput) {
        searchInput.addEventListener('input', (e) => {
            searchQuery = e.target.value;
            currentPage = 1;
            loadTours();
        });
    }
}

async function loadTours() {
    const data = await getTours(searchQuery, currentPage);
    const container = document.getElementById('tour-list-customer');
    if (container) {
        container.innerHTML = data.items.map(t => `
            <div class="tour-card">
                <img src="${t.hinhAnh || 'https://via.placeholder.com/400x200'}" alt="${t.tenTour}">
                <div class="tour-card-content">
                    <h3>${t.tenTour}</h3>
                    <p style="color: #666; margin: 10px 0;">${t.moTa || ''}</p>
                    <p style="font-size: 20px; font-weight: bold; color: #667eea;">${formatMoney(t.gia)}</p>
                    <button class="btn btn-primary mt-3" onclick="viewTourDetail(${t.maTour})">Xem chi tiết</button>
                </div>
            </div>
        `).join('');
    }
    const paginationContainer = document.getElementById('tour-pagination-customer');
    if (paginationContainer) {
        paginationContainer.innerHTML = `
            <button class="btn btn-primary" ${currentPage <= 1 ? 'disabled' : ''} onclick="changePage(${currentPage - 1})">Trước</button>
            <span style="padding: 10px;">Trang ${currentPage} / ${data.totalPages}</span>
            <button class="btn btn-primary" ${currentPage >= data.totalPages ? 'disabled' : ''} onclick="changePage(${currentPage + 1})">Sau</button>
        `;
    }
}

window.viewTourDetail = function(id) {
    navigate('tour-detail', { id });
};

window.changePage = function(page) {
    currentPage = page;
    loadTours();
};
