import { apiRequest } from '../api.js';

export async function getDestinations() {
    return apiRequest('/diemden');
}

export async function getDestinationById(id) {
    return apiRequest(`/diemden/${id}`);
}

export async function createDestination(formData) {
    return apiRequest('/diemden', {
        method: 'POST',
        body: formData
    });
}

export async function updateDestination(id, formData) {
    return apiRequest(`/diemden/${id}`, {
        method: 'PUT',
        body: formData
    });
}

export async function deleteDestination(id) {
    return apiRequest(`/diemden/${id}`, {
        method: 'DELETE'
    });
}
