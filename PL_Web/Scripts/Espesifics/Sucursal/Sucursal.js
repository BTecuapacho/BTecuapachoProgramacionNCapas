$(document).ready(
    GetAll()
)

function GetAll() {
    $.ajax({
        url: urlGetAll,
        type: 'GET',
        dataType: 'JSON',
        success: function (data) {
            console.log(data)
            var contenedor = $('#ContenidoSucursal')
            if (data && data.Correct) {
                contenedor.empty()
                if (data.Objects != 0) {
                    contenedor.append(CuerpoGetAll())
                    $.each(data.Objects, function (contador, sucursal) {
                        sucursal.Contador = contador + 1
                        DrawRow(sucursal)
                    })
                    contenedor.append(CuerpoMapsMarcadores())
                    initMap(data.Objects);
                } else {
                    contenedor.append(SinResultados())
                }
            } else {
                AlertaModal('Error', `Error al obtener los productos: ${data.ErrorMessage}`, ErrorALERT);
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}

function DrawRow(sucursal) {
    let tr = `
    <tr>
        <td class="text-center">
            ${sucursal.Contador}
        </td>
        <td class="text-center">
            ${sucursal.Nombre}
        </td>
        <td class="text-center">
            <button id="${sucursal.IdSucursal}" name="editStock" 
                    class="rounded-circle btn btn-warning d-inline-flex align-items-center justify-content-center text-light m-1" 
                    onclick="GetStock(this)">
                <i class="bi bi-pen-fill"></i>
            </button>
            <button id="${sucursal.IdSucursal}"
                    class="rounded-circle btn btn-danger d-inline-flex align-items-center justify-content-center"
                    onclick="CancelEdit(this)">
                <i class="bi bi-trash3-fill"></i>
            </button>
        </td>
    </tr>`
    $('#tbodySucursal').append(tr)
}

function CuerpoGetAll() {
    return `
    <div class="col-md-6 col-sm-12 mb-4">
        <div class="card shadow">
            <div class="border-bottom title-part-padding">
                <h4 class="card-title my-2 text-center">Lista de sucursales</h4>
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <table class="table table-striped table-bordered">
                        <thead>
                            <tr>
                                <th class="text-center" scope="col">#</th>
                                <th class="text-center" scope="col">Sucursal</th>
                                <th class="text-center" scope="col">Acciones</th>
                            </tr>
                        </thead>
                        <tbody id="tbodySucursal"></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>`;
}

function CuerpoMapsMarcadores() {
    return `
    <div class="col-md-6 col-sm-12 mb-4">
        <div class="card shadow">
            <div class="border-bottom title-part-padding">
                <h4 class="card-title my-2 text-center">Mapa de sucursales</h4>
            </div>
            <div class="card-body">
                <div id="sucursalMapa" class="rounded-3 shadow" style="height: 35rem;"></div>
            </div>
        </div>
    </div>`;
}


function SinResultados() {
    return `
    <div class="col-md-12">
        <div class="card p-5 border shadow-lg">
            <div class="text-center">
                <h2 class="fw-bold">No se encontró ninguna sucursal</h2>
                <img src="/Content/imagenes/mapas.png" alt="SinSucursales" class="avatar-img m-5" width="25%" id="img">
            </div>
        </div>
    </div>`;
}

function initMap(sucursales) {
    if (!sucursales || sucursales.length === 0) return;

    var bounds = new google.maps.LatLngBounds();
    var mapOptions = {
        mapTypeId: 'roadmap'
    };

    var map = new google.maps.Map(document.getElementById('sucursalMapa'), mapOptions);
    map.setTilt(50);

    var infoWindow = new google.maps.InfoWindow();

    sucursales.forEach(function (sucursal) {
        var lat = parseFloat(sucursal.Latitud);
        var lng = parseFloat(sucursal.Longitud);
        var position = new google.maps.LatLng(lat, lng);
        bounds.extend(position);

        var marker = new google.maps.Marker({
            position: position,
            map: map,
            title: sucursal.Nombre
        });

        var contentString = `
            <div class="info_content">
                <h3>${sucursal.Nombre}</h3>
                <p>Lat: ${sucursal.Latitud}, Lng: ${sucursal.Longitud}</p>
            </div>`;

        marker.addListener('click', function () {
            infoWindow.setContent(contentString);
            infoWindow.open(map, marker);
        });
    });

    map.fitBounds(bounds);
}
