import { getTours } from '../services/tourService.js';
import { formatMoney } from '../utils.js';
import { navigate } from '../router.js';

let currentTours = [];

export async function initHome() {
  await loadTours();
  initFilters();
}

async function loadTours() {
  try {
    const data = await getTours('', 1, 100);
    currentTours = data.items || data || [];
    renderTours();
  } catch (error) {
    console.error('Error loading tours:', error);
  }
}

function formatPrice(price) {
  return price ? price.toLocaleString('vi-VN') + 'đ' : '0đ';
}

function renderTours(filterDays = 'all') {
  const tourGrid = document.getElementById('tourGrid');
  const tourCount = document.getElementById('tourCount');
  
  if (!tourGrid) return;

  let filtered = [...currentTours];

  if (filterDays !== 'all') {
    filtered = filtered.filter(t => t.soNgay === parseInt(filterDays));
  }

  if (tourCount) tourCount.innerText = filtered.length + ' tour';

  if (filtered.length === 0) {
    tourGrid.innerHTML = `<p style="grid-column:1/-1; text-align:center; padding:40px;">😢 Không có tour phù hợp</p>`;
    return;
  }

  tourGrid.innerHTML = filtered
    .map(tour => `
      <div class="tour-card" data-id="${tour.maTour}">
        <div class="tour-image" style="background-image: url('${tour.hinhAnh ? (tour.hinhAnh.startsWith('http') ? tour.hinhAnh : 'https://localhost:7077' + tour.hinhAnh) : 'https://via.placeholder.com/400x200'}')">
          ${tour.trangThai === 'Hot' ? `<span class="tour-badge">🔥 ${tour.trangThai}</span>` : ''}
        </div>
        <div class="tour-content">
          <h3 class="tour-title">${tour.tenTour || 'Tour không tên'}</h3>
          <div class="tour-location">📍 ${tour.tenDiemDen || ''}</div>
          <div class="tour-price">${formatPrice(tour.giaCoBan)} <small>/ người</small></div>
          ${tour.soNgay ? `<div class="tour-days">📅 ${tour.soNgay} ngày ${tour.soNgay - 1} đêm</div>` : ''}
          <p class="tour-description">${tour.moTa ? tour.moTa.substring(0, 70) + '...' : ''}</p>
          <div class="tour-footer">
            <button class="btn-detail" data-id="${tour.maTour}">🔍 Xem chi tiết</button>
          </div>
        </div>
      </div>
    `)
    .join('');

  document.querySelectorAll('.btn-detail').forEach(btn => {
    btn.addEventListener('click', e => {
      e.stopPropagation();
      const tourId = btn.getAttribute('data-id');
      navigate('tour-detail', { id: tourId });
    });
  });

  document.querySelectorAll('.tour-card').forEach(card => {
    card.addEventListener('click', () => {
      const tourId = card.getAttribute('data-id');
      navigate('tour-detail', { id: tourId });
    });
  });
}

function initFilters() {
  document.querySelectorAll('.filter-btn').forEach(btn => {
    btn.addEventListener('click', () => {
      document.querySelectorAll('.filter-btn').forEach(b => b.classList.remove('active'));
      btn.classList.add('active');
      const days = btn.getAttribute('data-days');
      renderTours(days);
    });
  });
}
