import { apiRequest } from '../api.js';

export async function getBookings(page = 1, pageSize = 10) {
    let url = `/bookings?page=${page}&pageSize=${pageSize}`;
    return apiRequest(url);
}

export async function getBookingById(id) {
    return apiRequest(`/bookings/${id}`);
}

export async function createBooking(data) {
    return apiRequest('/bookings', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    });
}

export async function updateBookingStatus(id, status) {
    return apiRequest(`/bookings/${id}/status?status=${encodeURIComponent(status)}`, {
        method: 'PUT',
    });
}

export async function getMyBookings() {
    return apiRequest('/bookings');
}
