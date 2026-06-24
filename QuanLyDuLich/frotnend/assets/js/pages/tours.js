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
    let data = { items: [], totalPages: 1 };
    try {
        data = await getTours(searchQuery, currentPage);
    } catch (e) {
        // Fallback fake data
        data = { items: [], totalPages: 1 };
    }
    
    const container = document.getElementById('tour-list');
    container.innerHTML = data.items.length > 0 
        ? data.items.map(t => `
        <div class="tour-card" style="background: white; border-radius: 24px; overflow: hidden; box-shadow: 0 10px 25px rgba(0,0,0,0.08); transition: transform 0.3s;">
            <div class="tour-image" style="height: 200px; background-size: cover; background-position: center; background-image: url('${t.hinhAnh ? (t.hinhAnh.startsWith('http') ? t.hinhAnh : 'https://localhost:7077' + t.hinhAnh) : 'https://via.placeholder.com/400x200'}');"></div>
            <div class="tour-content" style="padding: 18px;">
                <h3 style="color: #1a472a; margin: 0 0 10px 0;">${t.tenTour}</h3>
                <p style="color: #666; margin: 0 0 10px 0;">${t.moTa ? t.moTa.substring(0, 70) + '...' : ''}</p>
                <p style="font-size: 1.3rem; font-weight: bold; color: #ff9800; margin: 0 0 10px 0;">${formatMoney(t.giaCoBan || t.gia)}</p>
                <div style="display: flex; gap: 10px;">
                    <button onclick="editTour(${t.maTour})" style="flex: 1; background: #2d6a4f; color: white; border: none; padding: 10px; border-radius: 12px; font-weight: 600; cursor: pointer;">✏️ Sửa</button>
                    <button onclick="confirmDeleteTour(${t.maTour})" style="flex: 1; background: #a13e3e; color: white; border: none; padding: 10px; border-radius: 12px; font-weight: 600; cursor: pointer;">🗑️ Xóa</button>
                </div>
            </div>
        </div>
    `).join('')
    : '<p style="grid-column: 1/-1; text-align: center; padding: 40px; color: #666;">😢 Không có tour nào</p>';
    
    const paginationContainer = document.getElementById('tour-pagination');
    paginationContainer.innerHTML = `
        <button ${currentPage <= 1 ? 'disabled' : ''} onclick="changePage(${currentPage - 1})" style="background: white; border: 1px solid #ddd; padding: 10px 20px; border-radius: 20px; cursor: pointer; ${currentPage <=1 ? 'opacity:0.5; cursor:not-allowed' : ''};">← Trước</button>
        <span style="padding: 10px; color: #1a472a; font-weight: 600;">Trang ${currentPage} / ${data.totalPages}</span>
        <button ${currentPage >= data.totalPages ? 'disabled' : ''} onclick="changePage(${currentPage + 1})" style="background: white; border: 1px solid #ddd; padding: 10px 20px; border-radius: 20px; cursor: pointer; ${currentPage >= data.totalPages ? 'opacity:0.5; cursor:not-allowed' : ''};">Sau →</button>
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
