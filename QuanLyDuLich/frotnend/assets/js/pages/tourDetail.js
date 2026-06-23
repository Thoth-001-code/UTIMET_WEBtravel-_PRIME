import { getTourById } from '../services/tourService.js';
import { formatMoney, formatDate } from '../utils.js';
import { navigate } from '../router.js';
import { apiRequest } from '../api.js';

let currentTourId = null;

export async function initTourDetail(params) {
    currentTourId = params.id;
    const tour = await getTourById(currentTourId);
    const schedules = await apiRequest(`/schedules/by-tour/${currentTourId}`);

    const container = document.getElementById('tour-detail-content');
    container.innerHTML = `
        <div class="card">
            <img src="${tour.hinhAnh || 'https://via.placeholder.com/800x400'}" style="width: 100%; height: 400px; object-fit: cover; border-radius: 10px;">
            <h1 class="mt-3">${tour.tenTour}</h1>
            <p style="color: #666; margin: 10px 0;">${tour.moTa || ''}</p>
            <p style="font-size: 24px; font-weight: bold; color: #667eea;">${formatMoney(tour.gia)}</p>
            <p>Thời gian: ${tour.thoiGian}</p>
            <h3 class="mt-3">Lịch khởi hành</h3>
            <table class="mt-2">
                <thead>
                    <tr>
                        <th>Ngày khởi hành</th>
                        <th>Số chỗ còn lại</th>
                        <th>Hành động</th>
                    </tr>
                </thead>
                <tbody>
                    ${schedules.map(s => `
                        <tr>
                            <td>${formatDate(s.ngayKhoiHanh)}</td>
                            <td>${s.soChoConLai}</td>
                            <td>
                                <button class="btn btn-primary" onclick="bookTour(${s.maLich})" ${s.soChoConLai <= 0 ? 'disabled' : ''}>
                                    Đặt tour
                                </button>
                            </td>
                        </tr>
                    `).join('')}
                </tbody>
            </table>
        </div>
    `;
}

window.bookTour = function(scheduleId) {
    window.currentScheduleId = scheduleId;
    navigate('booking', { tourId: currentTourId, scheduleId });
};
