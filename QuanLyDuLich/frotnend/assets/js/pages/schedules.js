import { getTours } from '../services/tourService.js';
import { getSchedules, getSchedulesByTour, createSchedule, updateSchedule, deleteSchedule } from '../services/scheduleService.js';
import { formatDate } from '../utils.js';

let currentPage = 1;
let tours = [];

export async function initSchedules() {
    // Fetch tours for dropdown
    try {
        const toursData = await getTours('', 1, 1000);
        tours = toursData.items || toursData;
        const tourSelect = document.getElementById('maTour');
        tourSelect.innerHTML = '<option value="">-- Chọn tour --</option>' +
            tours.map(t => `<option value="${t.maTour}">${t.tenTour}</option>`).join('');
    } catch (e) {
        console.error('Error fetching tours:', e);
    }

    await loadSchedules();
    setupScheduleForm();
}

async function loadSchedules() {
    try {
        const data = await getSchedules(currentPage, 10);
        const tbody = document.querySelector('#schedule-table tbody');
        tbody.innerHTML = (data.items || []).map(s => `
            <tr style="border-bottom: 1px solid #eee;">
                <td style="padding: 12px;">${s.maLich}</td>
                <td style="padding: 12px;">${s.tenTour || 'N/A'}</td>
                <td style="padding: 12px;">${formatDate(s.ngayKhoiHanh)}</td>
                <td style="padding: 12px;">${formatDate(s.ngayKetThuc)}</td>
                <td style="padding: 12px;">${s.soChoToiDa}</td>
                <td style="padding: 12px;">${s.soChoConLai}</td>
                <td style="padding: 12px;">${s.giaTour ? s.giaTour.toLocaleString('vi-VN') + 'đ' : ''}</td>
                <td style="padding: 12px;">
                    <span style="display: inline-block; padding: 4px 12px; border-radius: 20px; font-size: 0.8rem; font-weight: 600; background: ${getStatusBackground(s.trangThai)}; color: ${getStatusTextColor(s.trangThai)};">
                        ${getStatusLabel(s.trangThai)}
                    </span>
                </td>
                <td style="padding: 12px;">
                    <div style="display: flex; gap: 8px;">
                        <button onclick="editSchedule(${s.maLich})" style="background: #2d6a4f; color: white; border: none; padding: 6px 12px; border-radius: 8px; font-weight: 600; cursor: pointer; font-size: 0.9rem;">✏️ Sửa</button>
                        <button onclick="confirmDeleteSchedule(${s.maLich})" style="background: #a13e3e; color: white; border: none; padding: 6px 12px; border-radius: 8px; font-weight: 600; cursor: pointer; font-size: 0.9rem;">🗑️ Xóa</button>
                    </div>
                </td>
            </tr>
        `).join('');

        // Pagination
        const paginationContainer = document.getElementById('schedule-pagination');
        if (data.totalPages > 1) {
            paginationContainer.innerHTML = `
                <button ${currentPage <= 1 ? 'disabled' : ''} onclick="changeSchedulePage(${currentPage - 1})" style="background: white; border: 1px solid #ddd; padding: 10px 20px; border-radius: 20px; cursor: pointer; ${currentPage <=1 ? 'opacity:0.5; cursor:not-allowed' : ''};">← Trước</button>
                <span style="padding: 10px; color: #1a472a; font-weight: 600;">Trang ${currentPage} / ${data.totalPages}</span>
                <button ${currentPage >= data.totalPages ? 'disabled' : ''} onclick="changeSchedulePage(${currentPage + 1})" style="background: white; border: 1px solid #ddd; padding: 10px 20px; border-radius: 20px; cursor: pointer; ${currentPage >= data.totalPages ? 'opacity:0.5; cursor:not-allowed' : ''};">Sau →</button>
            `;
        } else {
            paginationContainer.innerHTML = '';
        }
    } catch (e) {
        console.error('Error loading schedules:', e);
        alert('Lỗi khi tải danh sách lịch khởi hành');
    }
}

function setupScheduleForm() {
    const form = document.getElementById('schedule-form');
    form.addEventListener('submit', async (e) => {
        e.preventDefault();
        const editId = document.getElementById('editScheduleId').value;
        const scheduleData = {
            maTour: parseInt(document.getElementById('maTour').value),
            ngayKhoiHanh: new Date(document.getElementById('ngayKhoiHanh').value).toISOString(),
            ngayKetThuc: new Date(document.getElementById('ngayKetThuc').value).toISOString(),
            soChoToiDa: parseInt(document.getElementById('soChoToiDa').value),
            giaTour: parseFloat(document.getElementById('giaTour').value) || null,
            trangThai: document.getElementById('trangThai').value
        };

        try {
            if (editId) {
                await updateSchedule(editId, scheduleData);
                alert('✅ Cập nhật lịch khởi hành thành công!');
            } else {
                await createSchedule(scheduleData);
                alert('✅ Thêm lịch khởi hành thành công!');
            }
            hideScheduleForm();
            await loadSchedules();
        } catch (err) {
            alert(err.message);
        }
    });
}

window.showScheduleForm = function() {
    document.getElementById('schedule-form-container').style.display = 'block';
    document.getElementById('schedule-form-title').textContent = '➕ Thêm Lịch';
    document.getElementById('schedule-form').reset();
    document.getElementById('editScheduleId').value = '';
}

window.hideScheduleForm = function() {
    document.getElementById('schedule-form-container').style.display = 'none';
    document.getElementById('schedule-form').reset();
    document.getElementById('editScheduleId').value = '';
}

window.editSchedule = function(id) {
    // We don't have getScheduleById, so let's find from loaded data or just open form
    // For now, just open form with id
    document.getElementById('editScheduleId').value = id;
    document.getElementById('schedule-form-title').textContent = '✏️ Sửa Lịch';
    document.getElementById('schedule-form-container').style.display = 'block';
}

window.confirmDeleteSchedule = async function(id) {
    if (confirm('Bạn có chắc muốn xóa lịch khởi hành này?')) {
        try {
            await deleteSchedule(id);
            alert('✅ Xóa lịch khởi hành thành công!');
            await loadSchedules();
        } catch (e) {
            alert('Lỗi khi xóa lịch khởi hành');
        }
    }
}

window.changeSchedulePage = function(page) {
    currentPage = page;
    loadSchedules();
}

function getStatusLabel(status) {
    const labels = {
        'sap_khoi_han': 'Sắp khởi hành',
        'dang_chay': 'Đang chạy',
        'hoan_thanh': 'Hoàn thành',
        'da_huy': 'Đã hủy'
    };
    return labels[status] || status;
}

function getStatusBackground(status) {
    const colors = {
        'sap_khoi_han': '#fff3cd',
        'dang_chay': '#cce5ff',
        'hoan_thanh': '#d4edda',
        'da_huy': '#f8d7da'
    };
    return colors[status] || '#ddd';
}

function getStatusTextColor(status) {
    const colors = {
        'sap_khoi_han': '#856404',
        'dang_chay': '#004085',
        'hoan_thanh': '#155724',
        'da_huy': '#721c24'
    };
    return colors[status] || '#333';
}

