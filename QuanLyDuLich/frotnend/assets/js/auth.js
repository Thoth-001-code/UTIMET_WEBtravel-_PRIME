import { apiRequest } from './api.js';

let currentUser = null;

async function login(email, password) {
    const data = await apiRequest('/auth/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ email, matKhau: password }),
    });
    localStorage.setItem('token', data.token);
    localStorage.setItem('user', JSON.stringify(data.user));
    currentUser = data.user;
    return data;
}

async function register(email, password, fullName, phone) {
    const data = await apiRequest('/auth/register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ email, matKhau: password, hoTen: fullName, soDienThoai: phone }),
    });
    return data;
}

function logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    currentUser = null;
}

async function getCurrentUser() {
    if (!currentUser && localStorage.getItem('token')) {
        const storedUser = localStorage.getItem('user');
        if (storedUser) {
            currentUser = JSON.parse(storedUser);
        } else {
            try {
                currentUser = await apiRequest('/auth/me');
                localStorage.setItem('user', JSON.stringify(currentUser));
            } catch (e) {
                logout();
            }
        }
    }
    return currentUser;
}

function isAuthenticated() {
    return !!localStorage.getItem('token');
}

function isAdmin() {
    return currentUser?.vaiTro === 'quan_tri' || currentUser?.vaiTro === 'quan_ly';
}

export { login, register, logout, getCurrentUser, isAuthenticated, isAdmin };
