import { getTourById } from '../services/tourService.js';
import { getSchedulesByTour } from '../services/scheduleService.js';
import { formatMoney, formatDate } from '../utils.js';
import { navigate } from '../router.js';
import { apiRequest } from '../api.js';
import { isAuthenticated } from '../auth.js';

let currentTourId = null;

export async function initTourDetail(params) {
  currentTourId = params.id;
  const tour = await getTourById(currentTourId);
  let schedules = [];
  try {
    schedules = await getSchedulesByTour(currentTourId);
  } catch (e) {
    console.error('Error loading schedules:', e);
  }
  
  const mainContent = document.getElementById('main-content');
  mainContent.innerHTML = `
    <div class="container" style="padding: 40px 20px;">
      <button onclick="navigate('home'); return false;" class="back-home-btn" style="background: white; border: 1px solid #1e5a88; color: #1e5a88; font-size: 0.85rem; cursor: pointer; display: inline-flex; align-items: center; gap: 6px; padding: 8px 18px; border-radius: 40px; margin-bottom: 20px;">← Quay lại trang chủ</button>
      
      <div class="card" style="background: white; border-radius: 24px; padding: 30px; box-shadow: 0 10px 25px rgba(0,0,0,0.08); margin-bottom: 24px;">
        <div style="display: grid; grid-template-columns: 1fr 1fr; gap: 30px; align-items: start;">
          <div>
            <div style="width:100%; height:300px; border-radius:20px; background-size:cover; background-position:center; background-image: url('${tour.hinhAnh ? (tour.hinhAnh.startsWith('http') ? tour.hinhAnh : 'https://localhost:7077' + tour.hinhAnh) : 'https://via.placeholder.com/600x300'}');"></div>
          </div>
          <div>
            <h1 style="color: #1a472a; font-size: 2rem; margin-bottom: 10px;">${tour.tenTour}</h1>
            <div style="display: flex; gap: 10px; margin-bottom: 10px; flex-wrap: wrap;">
              ${tour.tenDiemDen ? `<span style="background: #e8f4fd; padding: 5px 12px; border-radius: 20px; font-size: 0.9rem; color: #1e5a88;">📍 ${tour.tenDiemDen}</span>` : ''}
              ${tour.soNgay ? `<span style="background: #fdf8ed; padding: 5px 12px; border-radius: 20px; font-size: 0.9rem; color: #bc6f1a;">📅 ${tour.soNgay} ngày ${tour.soNgay - 1} đêm</span>` : ''}
            </div>
            <p style="color: #666; line-height: 1.6; margin-bottom: 20px;">${tour.moTa || ''}</p>
            <div style="font-size: 1.8rem; font-weight: bold; color: #ff9800; margin-bottom: 20px;">${formatMoney(tour.giaCoBan)} <span style="font-size:1rem; font-weight:normal; color:#888;">/ người</span></div>
          </div>
        </div>
      </div>

      <div class="card" style="background: white; border-radius: 24px; padding: 30px; box-shadow: 0 10px 25px rgba(0,0,0,0.08);">
        <h2 style="color: #1a472a; font-size: 1.5rem; margin-bottom: 20px;">📅 Lịch khởi hành</h2>
        ${schedules.length === 0 ? `
          <p style="color: #666; text-align: center; padding: 40px;">Chưa có lịch khởi hành cho tour này</p>
        ` : `
          <div style="display: grid; gap: 16px;">
            ${schedules.map(schedule => `
              <div style="border: 1px solid #e2e8f0; border-radius: 16px; padding: 20px; display: flex; justify-content: space-between; align-items: center; flex-wrap: wrap; gap: 16px;">
                <div>
                  <div style="font-weight: 600; color: #1a472a; margin-bottom: 4px;">
                    Ngày khởi hành: ${formatDate(schedule.ngayKhoiHanh)}
                  </div>
                  <div style="color: #666; font-size: 0.9rem; margin-bottom: 4px;">
                    Ngày kết thúc: ${formatDate(schedule.ngayKetThuc)}
                  </div>
                  <div style="color: #666; font-size: 0.9rem; margin-bottom: 4px;">
                    Còn ${schedule.soChoConLai} chỗ trống
                  </div>
                  <div style="font-size: 1.1rem; font-weight: 700; color: #ff9800;">
                    ${schedule.giaTour ? formatMoney(schedule.giaTour) + ' / người' : formatMoney(tour.giaCoBan) + ' / người'}
                  </div>
                </div>
                <div>
                  ${isAuthenticated() ? `
                    <button onclick="navigate('booking', { scheduleId: ${schedule.maLich} }); return false;"
                      style="background: linear-gradient(135deg, #1e5a88, #144d74); color: white; border: none; padding: 10px 24px; border-radius: 30px; font-weight: 700; cursor: pointer; font-size: 1rem;"
                      ${schedule.soChoConLai <= 0 ? 'disabled style="background: #ccc; cursor: not-allowed;"' : ''}>
                      ${schedule.soChoConLai <= 0 ? 'Hết chỗ' : 'Đặt tour'}
                    </button>
                  ` : `
                    <button onclick="navigate('login'); return false;"
                      style="background: linear-gradient(135deg, #1e5a88, #144d74); color: white; border: none; padding: 10px 24px; border-radius: 30px; font-weight: 700; cursor: pointer; font-size: 1rem;">
                      Đăng nhập để đặt tour
                    </button>
                  `}
                </div>
              </div>
            `).join('')}
          </div>
        `}
      </div>
    </div>
  `;
}
