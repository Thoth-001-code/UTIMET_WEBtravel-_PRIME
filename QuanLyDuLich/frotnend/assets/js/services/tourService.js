import { apiRequest } from '../api.js';

export async function getTours(search = '', page = 1, pageSize = 10) {
    let url = `/tours?page=${page}&pageSize=${pageSize}`;
    if (search) {
        url += `&search=${encodeURIComponent(search)}`;
    }
    return apiRequest(url);
}

export async function getTourById(id) {
    return apiRequest(`/tours/${id}`);
}

export async function createTour(formData) {
    return apiRequest('/tours', {
        method: 'POST',
        body: formData,
    });
}

export async function updateTour(id, formData) {
    return apiRequest(`/tours/${id}`, {
        method: 'PUT',
        body: formData,
    });
}

export async function deleteTour(id) {
    return apiRequest(`/tours/${id}`, {
        method: 'DELETE',
    });
}
