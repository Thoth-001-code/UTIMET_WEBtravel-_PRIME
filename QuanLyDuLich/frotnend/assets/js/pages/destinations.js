import { getDestinations, getDestinationById, createDestination, updateDestination, deleteDestination } from '../services/destinationService.js';

export async function initDestinations() {
    await loadDestinations();
    setupDestinationForm();
}

async function loadDestinations() {
    try {
        const data = await getDestinations();
        const tbody = document.querySelector('#destination-table tbody');
        tbody.innerHTML = data.map(d => `
            <tr style="border-bottom: 1px solid #eee;">
                <td style="padding: 12px;">${d.maDiemDen}</td>
                <td style="padding: 12px;">${d.tenDiemDen}</td>
                <td style="padding: 12px;">${d.quocGia || ''}</td>
                <td style="padding: 12px;">${d.thanhPho || ''}</td>
                <td style="padding: 12px;">${d.moTa || ''}</td>
                <td style="padding: 12px;">
                    <div style="display: flex; gap: 8px;">
                        <button onclick="editDestination(${d.maDiemDen})" style="background: #2d6a4f; color: white; border: none; padding: 6px 12px; border-radius: 8px; font-weight: 600; cursor: pointer; font-size: 0.9rem;">✏️ Sửa</button>
                        <button onclick="confirmDeleteDestination(${d.maDiemDen})" style="background: #a13e3e; color: white; border: none; padding: 6px 12px; border-radius: 8px; font-weight: 600; cursor: pointer; font-size: 0.9rem;">🗑️ Xóa</button>
                    </div>
                </td>
            </tr>
        `).join('');
    } catch (e) {
        console.error('Error loading destinations:', e);
        alert('Lỗi khi tải danh sách điểm đến');
    }
}

function setupDestinationForm() {
    const form = document.getElementById('destination-form');
    form.addEventListener('submit', async (e) => {
        e.preventDefault();
        const formData = new FormData(form);
        const editId = document.getElementById('editDestinationId').value;

        try {
            if (editId) {
                await updateDestination(editId, formData);
                alert('✅ Cập nhật điểm đến thành công!');
            } else {
                await createDestination(formData);
                alert('✅ Thêm điểm đến thành công!');
            }
            hideDestinationForm();
            await loadDestinations();
        } catch (err) {
            alert(err.message);
        }
    });
}

window.showDestinationForm = function() {
    document.getElementById('destination-form-container').style.display = 'block';
    document.getElementById('destination-form-title').textContent = '➕ Thêm Điểm đến';
    document.getElementById('destination-form').reset();
    document.getElementById('editDestinationId').value = '';
}

window.hideDestinationForm = function() {
    document.getElementById('destination-form-container').style.display = 'none';
    document.getElementById('destination-form').reset();
    document.getElementById('editDestinationId').value = '';
}

window.editDestination = async function(id) {
    try {
        const destination = await getDestinationById(id);
        document.getElementById('editDestinationId').value = destination.maDiemDen;
        document.getElementById('tenDiemDen').value = destination.tenDiemDen;
        document.getElementById('quocGia').value = destination.quocGia || '';
        document.getElementById('thanhPho').value = destination.thanhPho || '';
        document.getElementById('moTa').value = destination.moTa || '';
        document.getElementById('destination-form-title').textContent = '✏️ Sửa Điểm đến';
        document.getElementById('destination-form-container').style.display = 'block';
    } catch (e) {
        alert('Lỗi khi tải thông tin điểm đến');
    }
}

window.confirmDeleteDestination = async function(id) {
    if (confirm('Bạn có chắc muốn xóa điểm đến này?')) {
        try {
            await deleteDestination(id);
            alert('✅ Xóa điểm đến thành công!');
            await loadDestinations();
        } catch (e) {
            alert('Lỗi khi xóa điểm đến');
        }
    }
}

