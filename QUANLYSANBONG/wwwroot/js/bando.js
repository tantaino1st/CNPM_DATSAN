let map;
let markers = L.markerClusterGroup();
let allSanBong = [];
let filteredSanBong = [];
let currentLayer;

const osmLayer = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; OpenStreetMap contributors'
});

const googleLayer = L.tileLayer('http://{s}.google.com/vt/lyrs=m&x={x}&y={y}&z={z}', {
    maxZoom: 20,
    subdomains: ['mt0', 'mt1', 'mt2', 'mt3']
});

const satelliteLayer = L.tileLayer('http://{s}.google.com/vt/lyrs=s&x={x}&y={y}&z={z}', {
    maxZoom: 20,
    subdomains: ['mt0', 'mt1', 'mt2', 'mt3']
});

document.addEventListener('DOMContentLoaded', function() {
    initMap();
    loadSanBong();
    initEventListeners();
});

function initMap() {
    map = L.map('map', {
        zoomControl: false // Custom zoom buttons
    }).setView([10.7769, 106.7009], 13); // Default to TP.HCM

    osmLayer.addTo(map);
    currentLayer = osmLayer;
    map.addLayer(markers);
}

function loadSanBong() {
    fetch('/BanDo/GetSanBong')
        .then(response => response.json())
        .then(data => {
            allSanBong = data;
            filteredSanBong = data;
            updateUI();
            
            // Check URL for maSan parameter
            const urlParams = new URLSearchParams(window.location.search);
            const targetMaSan = urlParams.get('maSan');
            if (targetMaSan) {
                const target = allSanBong.find(s => s.maSan == targetMaSan);
                if (target) {
                    setTimeout(() => focusField(target.lat, target.lng, target.maSan), 500);
                }
            }
        })
        .catch(err => console.error('Lỗi load dữ liệu sân bóng:', err));
}

function updateUI() {
    // 1. Clear current markers
    markers.clearLayers();
    
    // 2. Update Sidebar List
    const listContainer = document.getElementById('field-list');
    listContainer.innerHTML = '';
    
    document.getElementById('count-label').innerText = `${filteredSanBong.length} cơ sở`;

    filteredSanBong.forEach(san => {
        // Add Marker
        const marker = L.marker([san.lat, san.lng]);
        
        const popupContent = `
            <div class="popup-card">
                <img src="${san.hinhAnh || 'https://via.placeholder.com/240x120'}" class="popup-img">
                <div class="popup-body">
                    <div class="popup-title">${san.tenSan}</div>
                    <div class="popup-addr"><i class="fa-solid fa-location-dot me-1"></i> ${san.diaChi || san.tinhThanh}</div>
                    <div class="d-flex justify-content-between align-items-center mb-2">
                        <span class="badge bg-success-subtle text-success border border-success-subtle">${san.loaiSan}</span>
                        <span class="fw-bold text-primary">${san.gia.toLocaleString()}đ</span>
                    </div>
                    <div class="d-flex gap-2">
                        <a href="/DatSan/CreateClient?maSan=${san.maSan}" class="btn btn-primary btn-sm w-100 rounded-pill">Đặt sân</a>
                        <a href="/SanBong/Details/${san.maSan}" class="btn btn-outline-secondary btn-sm w-100 rounded-pill">Chi tiết</a>
                    </div>
                </div>
            </div>
        `;
        
        marker.bindPopup(popupContent);
        markers.addLayer(marker);

        // Add Card to Sidebar
        const card = document.createElement('div');
        card.className = 'map-field-card';
        card.innerHTML = `
            <img src="${san.hinhAnh || 'https://via.placeholder.com/320x140'}" class="card-img-top">
            <div class="card-body-custom">
                <div class="card-title-custom text-truncate">${san.tenSan}</div>
                <div class="card-text-custom text-truncate">
                    <i class="fa-solid fa-location-dot me-1 text-danger"></i> ${san.diaChi || san.tinhThanh}
                </div>
                <div class="d-flex justify-content-between align-items-center">
                    <span class="small text-muted"><i class="fa-solid fa-futbol me-1"></i> ${san.soSanCon} sân con</span>
                    <span class="fw-bold text-primary">${san.gia.toLocaleString()}đ</span>
                </div>
                <div class="d-flex gap-2 mt-3">
                    <a href="/DatSan/CreateClient?maSan=${san.maSan}" class="btn btn-success btn-sm w-100 rounded-3">Đặt sân</a>
                    <button onclick="focusField(${san.lat}, ${san.lng}, ${san.maSan})" class="btn btn-outline-primary btn-sm w-100 rounded-3">Xem vị trí</button>
                </div>
            </div>
        `;
        
        card.onclick = (e) => {
            if (e.target.tagName !== 'A' && e.target.tagName !== 'BUTTON') {
                focusField(san.lat, san.lng, san.maSan);
            }
        };
        
        listContainer.appendChild(card);
    });
}

function focusField(lat, lng, maSan) {
    map.setView([lat, lng], 17, { animate: true });
    
    // Find and open popup
    markers.eachLayer(marker => {
        const mLat = marker.getLatLng().lat;
        const mLng = marker.getLatLng().lng;
        if (Math.abs(mLat - lat) < 0.0001 && Math.abs(mLng - lng) < 0.0001) {
            marker.openPopup();
        }
    });
}

function initEventListeners() {
    // Search
    document.getElementById('map-search').oninput = function(e) {
        const keyword = e.target.value.toLowerCase();
        filterSanBong(keyword, document.querySelector('.chip.active').dataset.type);
    };

    // Filter Chips
    document.querySelectorAll('.chip').forEach(chip => {
        chip.onclick = function() {
            document.querySelector('.chip.active').classList.remove('active');
            this.classList.add('active');
            filterSanBong(document.getElementById('map-search').value.toLowerCase(), this.dataset.type);
        };
    });

    // Layer Switch
    document.getElementById('btn-osm').onclick = () => switchLayer(osmLayer);
    document.getElementById('btn-google').onclick = () => switchLayer(googleLayer);
    document.getElementById('btn-satellite').onclick = () => switchLayer(satelliteLayer);

    // Zoom Controls
    document.getElementById('btn-zoom-in').onclick = () => map.zoomIn();
    document.getElementById('btn-zoom-out').onclick = () => map.zoomOut();

    // Geolocation
    document.getElementById('btn-locate').onclick = function() {
        map.locate({setView: true, maxZoom: 16});
    };

    map.on('locationfound', function(e) {
        L.circle(e.latlng, e.accuracy).addTo(map);
        L.marker(e.latlng).addTo(map).bindPopup("Bạn đang ở đây").openPopup();
    });
}

function filterSanBong(keyword, type) {
    filteredSanBong = allSanBong.filter(san => {
        const matchKeyword = !keyword || 
            san.tenSan.toLowerCase().includes(keyword) || 
            (san.diaChi && san.diaChi.toLowerCase().includes(keyword)) ||
            (san.tinhThanh && san.tinhThanh.toLowerCase().includes(keyword));
            
        const matchType = type === 'Tất cả' || san.loaiSan === type;
        
        return matchKeyword && matchType;
    });
    
    updateUI();
}

function switchLayer(layer) {
    map.removeLayer(currentLayer);
    layer.addTo(map);
    currentLayer = layer;
}
