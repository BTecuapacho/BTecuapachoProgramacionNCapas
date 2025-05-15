let map;
let marcador = null;
const ubicacionPorDefecto = { lat: 19.4326, lng: -99.1332 };

$(document).ready(function () {
    //initMap();
});

function initMap() {
    const idSucursal = $("#IdSucursal").val();
    const lat = parseFloat($("#Latitud").val());
    const lng = parseFloat($("#Longitud").val());

    const coordenadasValidas = !isNaN(lat) && !isNaN(lng);
    const ubicacionInicial = (idSucursal && coordenadasValidas) ? { lat, lng } : ubicacionPorDefecto;

    map = new google.maps.Map(document.getElementById('sucursalMapa'), {
        center: ubicacionInicial,
        zoom: 12
    });

    if (idSucursal && coordenadasValidas) {
        addMarker(ubicacionInicial);
    }

    map.addListener('click', function (event) {
        const location = {
            lat: event.latLng.lat(),
            lng: event.latLng.lng()
        };
        addMarker(location);
        updateInputs(location);
    });
}

function addMarker(location) {
    if (marcador) {
        marcador.setMap(null);
    }

    marcador = new google.maps.Marker({
        position: location,
        map: map
    });

    map.setCenter(location);
}

function updateInputs(location) {
    $("#Latitud").val(location.lat);
    $("#Longitud").val(location.lng);
}

function ValidateForm() {
    var nombre = $("#Nombre").val();
    var latitud = $("#Latitud").val();
    var longitud = $("#Longitud").val();
    if (nombre === "" || latitud === "" || longitud === "") {
        return false;
    } else {
        return true;
    }
}

function Enviar(event) {
    if (!ValidateForm()) {
        event.preventDefault();
        AlertaModal('Formulario no valido', 'Por favor, complete todos los campos del formulario.', WarningALERT);
    }
}