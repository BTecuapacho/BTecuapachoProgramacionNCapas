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
            if (data && data.Correct) {

            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}

function DrawRow(sucursal) {
    let tr = `
    <tr data-id="${productoSucursal.IdProductoSucursal}" data-original-stock="${productoSucursal.Stock}">
        <td class="text-center">${productoSucursal.Contador}</td>
        <td class="text-center">
            <div class="text-center mb-1">
                <img style="width: 15%; filter: drop-shadow(4px 4px 4px rgba(0, 0, 0, 0.61));" 
                     alt="Imagen ${productoSucursal.Producto.Nombre}" 
                     src="${src}" 
                     class="me-1 rounded-2" />
                ${productoSucursal.Producto.Nombre}
            </div>
        </td>
        <td class="text-center">
            <i class="bi bi-buildings"></i> ${productoSucursal.Sucursal.Nombre}
        </td>
        <td class="text-center" id="editarStock-${productoSucursal.IdProductoSucursal}">
            <i class="bi bi-boxes"></i> ${productoSucursal.Stock}
        </td>
        <td class="text-center">
            <div class="text-center buttonEdit-${productoSucursal.IdProductoSucursal} d-block">
                <button id="${productoSucursal.IdProductoSucursal}" 
                        name="editStock" 
                        class="rounded-circle btn btn-warning d-inline-flex align-items-center justify-content-center text-light m-1" 
                        onclick="GetStock(this)">
                    <i class="bi bi-pen-fill"></i>
                </button>
            </div>
            <div class="text-center buttonsActinos-${productoSucursal.IdProductoSucursal} d-none">
                <button class="rounded-circle btn btn-primary d-inline-flex align-items-center justify-content-center text-light m-1"
                        onclick="ConfirmUpdateStock(this)">
                    <i class="bi bi-floppy2-fill"></i>
                </button>
                <button class="rounded-circle btn btn-danger d-inline-flex align-items-center justify-content-center"
                        onclick="CancelEdit(this)">
                    <i class="bi bi-x-square-fill"></i>
                </button>
            </div>
        </td>
    </tr>`
    $('#tbodySucursal').append(tr)
}

function CuerpoGetAll() {
    return `
    <div class="col-6">
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
                                <th class="text-center" scope="col">Altitud y Longitud</th>
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
    <div class="col-6">
        <div class="card shadow">
            <div class="border-bottom title-part-padding">
                <h4 class="card-title my-2 text-center">Mapa de sucursales</h4>
            </div>
            <div class="card-body">
                <div id="sucursalMapa">
                </div>
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
                <img src="~/Content/imagenes/mapas.png" alt="SinProsuctos" class="avatar-img m-5" width="25%" id="img">
            </div>
        </div>
    </div>`;
}