import { registerRoute, render } from './router.js';
import { login, register } from './auth.js';
import { initDashboard } from './pages/dashboard.js';
import { initTours } from './pages/tours.js';
import { initHome } from './pages/home.js';
import { initTourDetail } from './pages/tourDetail.js';
import { initBookingForm } from './pages/bookingForm.js';
import { initMyBookings } from './pages/myBookings.js';
import { initDestinations } from './pages/destinations.js';
import { initSchedules } from './pages/schedules.js';
import { initBookings } from './pages/bookings.js';
import { initCustomers } from './pages/customers.js';
import { initStaffs } from './pages/staffs.js';
import { getTourById, createTour, updateTour } from './services/tourService.js';
import { getDestinations } from './services/destinationService.js';
import { navigate } from './router.js';

registerRoute('login', async () => {
    document.getElementById('login-form').addEventListener('submit', async (e) => {
        e.preventDefault();
        try {
            await login(document.getElementById('email').value, document.getElementById('password').value);
            window.location.reload();
        } catch (err) {
            alert(err.message);
        }
    });
});

registerRoute('register', async () => {
    document.getElementById('register-form').addEventListener('submit', async (e) => {
        e.preventDefault();
        try {
            await register(
                document.getElementById('email').value,
                document.getElementById('password').value,
                document.getElementById('fullName').value,
                document.getElementById('phone').value
            );
            alert('Đăng ký thành công! Vui lòng đăng nhập.');
            navigate('login');
        } catch (err) {
            alert(err.message);
        }
    });
});

registerRoute('dashboard', initDashboard);
registerRoute('tours', initTours);
registerRoute('tour-edit', async (params) => {
    const form = document.getElementById('tour-form');
    const title = document.getElementById('tour-form-title');
    const destinationSelect = document.getElementById('maDiemDen');
    let tour = null;
    let destinations = [];

    // Fetch destinations for dropdown
    try {
        destinations = await getDestinations();
        destinationSelect.innerHTML = '<option value="">-- Chọn điểm đến --</option>' +
            destinations.map(d => `<option value="${d.maDiemDen}">${d.tenDiemDen}</option>`).join('');
    } catch (e) {
        console.error('Error fetching destinations:', e);
    }

    if (params.id) {
        try {
            tour = await getTourById(params.id);
            document.getElementById('tenTour').value = tour.tenTour || '';
            document.getElementById('moTa').value = tour.moTa || '';
            document.getElementById('giaCoBan').value = tour.giaCoBan || '';
            document.getElementById('soNgay').value = tour.soNgay || '';
            if (tour.maDiemDen) {
                destinationSelect.value = tour.maDiemDen;
            }
            document.getElementById('trangThai').value = tour.trangThai || 'Thường';
            title.textContent = '✏️ Sửa Tour';
        } catch (e) {
            title.textContent = '➕ Thêm Tour';
        }
    } else {
        title.textContent = '➕ Thêm Tour';
    }

    form.addEventListener('submit', async (e) => {
        e.preventDefault();
        const formData = new FormData(form);
        try {
            if (params.id) {
                await updateTour(params.id, formData);
            } else {
                await createTour(formData);
            }
            alert('✅ Lưu tour thành công!');
            navigate('tours');
        } catch (err) {
            alert(err.message);
        }
    });
});
registerRoute('destinations', initDestinations);
registerRoute('schedules', initSchedules);
registerRoute('bookings', initBookings);
registerRoute('customers', initCustomers);
registerRoute('staffs', initStaffs);
registerRoute('home', initHome);
registerRoute('tour-detail', initTourDetail);
registerRoute('booking', initBookingForm);
registerRoute('my-bookings', initMyBookings);

window.addEventListener('DOMContentLoaded', async () => {
    const hash = window.location.hash.slice(1) || 'home';
    const path = hash.split('?')[0];
    await render(path);
});

window.addEventListener('popstate', async (e) => {
    const path = e.state?.path || 'home';
    await render(path, e.state?.params);
});
