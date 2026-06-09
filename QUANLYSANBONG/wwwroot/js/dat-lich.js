document.addEventListener('DOMContentLoaded', function() {
    // 1. Tự động submit form khi đổi sân
    const selectSan = document.getElementById('maSanFilter');
    if (selectSan) {
        selectSan.addEventListener('change', function() {
            this.form.submit();
        });
    }

    // 2. Phím tắt
    document.addEventListener('keydown', function(e) {
        // Tránh kích hoạt phím tắt khi đang gõ vào input
        if (['INPUT', 'TEXTAREA', 'SELECT'].includes(e.target.tagName)) return;

        switch(e.key) {
            case 'ArrowLeft':
                const prev = document.getElementById('btnPrevWeek');
                if(prev) prev.click();
                break;
            case 'ArrowRight':
                const next = document.getElementById('btnNextWeek');
                if(next) next.click();
                break;
            case 't':
            case 'T':
                window.location.href = '/DatLich';
                break;
            case 'n':
            case 'N':
                const modal = new bootstrap.Modal(document.getElementById('quickBookingModal'));
                modal.show();
                break;
        }
    });

    // 3. Tự động làm mới dữ liệu sau 60s
    setTimeout(function() {
        // Chỉ reload nếu không có modal nào đang mở
        const modals = document.querySelectorAll('.modal.show');
        if (modals.length === 0) {
            location.reload();
        }
    }, 60000);
});

// Hàm mở Modal đặt lịch từ Timeline
function openQuickBooking(maSan, gio) {
    const modalEl = document.getElementById('quickBookingModal');
    const modal = new bootstrap.Modal(modalEl);
    
    // Điền dữ liệu mặc định vào form
    document.getElementById('modalMaSan').value = maSan;
    
    // Format thời gian HH:mm
    const formatTime = (h) => (h < 10 ? '0' + h : h) + ':00';
    
    document.getElementById('modalGioBatDau').value = formatTime(gio);
    document.getElementById('modalGioKetThuc').value = formatTime(gio + 1);
    
    modal.show();
}

// Hàm xem chi tiết và duyệt từ Timeline
function openBookingDetails(maDatSan) {
    window.location.href = '/DatSan/Details/' + maDatSan;
}
