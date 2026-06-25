import { apiRequest } from '../api.js';

export async function getSchedules(page = 1, pageSize = 10) {
    return apiRequest(`/schedules?page=${page}&pageSize=${pageSize}`);
}

export async function getSchedulesByTour(tourId) {
    return apiRequest(`/schedules/by-tour/${tourId}`);
}

export async function createSchedule(scheduleData) {
    return apiRequest('/schedules', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(scheduleData)
    });
}

export async function updateSchedule(id, scheduleData) {
    return apiRequest(`/schedules/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(scheduleData)
    });
}

export async function deleteSchedule(id) {
    return apiRequest(`/schedules/${id}`, {
        method: 'DELETE'
    });
}
