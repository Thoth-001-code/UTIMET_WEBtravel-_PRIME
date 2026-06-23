import { getTours, deleteTour } from '../services/tourService.js';
import { formatMoney } from '../utils.js';
import { navigate } from '../router.js';

let currentPage = 1;
let searchQuery = '';

export async function initTours() {
    await loadTours();
    document.getElementById('search-tour').addEventListener('input', (e) => {
        searchQuery = e.target.value;
        currentPage = 1;
        loadTours();
    });
}

async function loadTours() {
    const data = await getTours(searchQuery, currentPage);
    const container = document.getElementById('tour-list');
    container.innerHTML = data.items.map(t => `
        <div class="tour-card">
            <img src="${t.hinhAnh || 'https://via.placeholder.com/400x200'}" alt="${t.tenTour}">
            <div class="tour-card-content">
                <h3>${t.tenTour}</h3>
                <p style="color: #666; margin: 10px 0;">${t.moTa || ''}</p>
                <p style="font-size: 20px; font-weight: bold; color: #667eea;">${formatMoney(t.gia)}</p>
                <div class="d-flex gap-2 mt-3">
                    <button class="btn btn-primary" onclick="editTour(${t.maTour})">Sửa</button>
                    <button class="btn btn-danger" onclick="confirmDeleteTour(${t.maTour})">Xóa</button>
                </div>
            </div>
        </div>
    `).join('');
    document.getElementById('tour-pagination').innerHTML = `
        <button class="btn btn-primary" ${currentPage <= 1 ? 'disabled' : ''} onclick="changePage(${currentPage - 1})">Trước</button>
        <span style="padding: 10px;">Trang ${currentPage} / ${data.totalPages}</span>
        <button class="btn btn-primary" ${currentPage >= data.totalPages ? 'disabled' : ''} onclick="changePage(${currentPage + 1})">Sau</button>
    `;
}

window.showTourForm = function() {
    window.currentEditTourId = null;
    navigate('tour-edit');
};

window.editTour = function(id) {
    window.currentEditTourId = id;
    navigate('tour-edit', { id });
};

window.confirmDeleteTour = async function(id) {
    if (confirm('Bạn có chắc muốn xóa tour này?')) {
        await deleteTour(id);
        loadTours();
    }
};

window.changePage = function(page) {
    currentPage = page;
    loadTours();
};
