import { apiRequest } from '../api.js';

export async function getCustomers(search = '', page = 1, pageSize = 10) {
    let url = `/customers?page=${page}&pageSize=${pageSize}`;
    if (search) {
        url += `&search=${encodeURIComponent(search)}`;
    }
    return apiRequest(url);
}

export async function getCustomerById(id) {
    return apiRequest(`/customers/${id}`);
}

export async function createCustomer(data) {
    return apiRequest('/customers', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    });
}

export async function updateCustomer(id, data) {
    return apiRequest(`/customers/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    });
}

export async function deleteCustomer(id) {
    return apiRequest(`/customers/${id}`, {
        method: 'DELETE',
    });
}
