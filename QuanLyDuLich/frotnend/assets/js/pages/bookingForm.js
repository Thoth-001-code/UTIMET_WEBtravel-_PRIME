import { createBooking } from '../services/bookingService.js';
import { navigate } from '../router.js';
import { getCurrentUser } from '../auth.js';

let currentScheduleId = null;

export async function initBookingForm(params) {
    currentScheduleId = params.scheduleId;
    if (!currentScheduleId) {
        alert('Không tìm thấy lịch khởi hành!');
        navigate('home');
        return;
    }

    const user = await getCurrentUser();
    const form = document.getElementById('booking-form');
    
    form.addEventListener('submit', async (e) => {
        e.preventDefault();
        try {
            await createBooking({
                maLich: currentScheduleId,
                maKhachHang: user.maKhachHang,
                soLuongNguoi: parseInt(document.getElementById('soNguoiDi').value),
                ghiChu: document.getElementById('ghiChu').value,
                danhSachNguoiDi: []
            });
            alert('Đặt tour thành công!');
            navigate('my-bookings');
        } catch (err) {
            alert(err.message);
        }
    });
}
