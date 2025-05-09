$(document).ready(
    GetAllSucursales()
)

function GetAllSucursales() {
    $.ajax({
        url: urlGetAllSucursales,
        type: 'GET',
        dataType: 'JSON',
        success: function (data) {
            if (data && data.Correct) {
                let ddlSucursal = $('#ddlSucursal').empty().append('<option value="">Seleccione una sucursal</option>')
                $.each(data.Objects, function (i, valor) {
                    let opcion = '<option value="' + valor.IdSucursal + '">' + valor.Nombre + '</option>'
                    ddlSucursal.append(opcion)
                })
            }
        },
        error: function (xhr) {
            console.log(xhr)
            AlertaModal('Error', 'Error al obtener las sucursales', ErrorALERT);
        }
    })
}

function GetAll() {
    var idSucursal = $('#ddlSucursal').val();
    if (idSucursal && idSucursal > 0) {
        $('#tbodyProductoSucursal').empty();
        $.ajax({
            url: `${urlGetAll}${idSucursal}`,
            type: 'GET',
            dataType: 'JSON',
            success: function (data) {
                var contenedor = $('#ContenidoStok');
                if (data.Correct) {
                    contenedor.empty();
                    if (data.objects != 0) {
                        contenedor.append(CuerpoGetAll());
                        $.each(data.Objects, function (contador, producto) {
                            producto.Contador = contador + 1;
                            DrawRow(producto);
                        });
                    } else {
                        contenedor.append(SinResultados());
                    }
                } else {
                    AlertaModal('Error', 'Error al obtener los productos', ErrorALERT);
                }
            },
            error: function (xhr) {
                console.log(xhr);
                AlertaModal('Error', 'Error al obtener los productos', ErrorALERT);
            }
        });
    } else {
        $('#ContenidoStok').empty().append(SinResultados());
    }
}

function DrawRow(productoSucursal) {
    var src = productoSucursal.Producto.ImagenBase64 != "" && productoSucursal.Producto.ImagenBase64 != null ? "data:image/png;base64," + productoSucursal.Producto.ImagenBase64 : "/content/imagenes/SinIngredientes.png";

    var tr = `
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
    </tr>`;
    $('#tbodyProductoSucursal').append(tr);
}

function CuerpoGetAll() {
    return `
    <div class="col-12">
        <div class="card shadow">
            <div class="border-bottom title-part-padding">
                <h4 class="card-title my-2 text-center">Lista de productos</h4>
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <table id="usuarios" class="table table-striped table-bordered">
                        <thead>
                            <tr>
                                <th class="text-center" scope="col">#</th>
                                <th class="text-center" scope="col">Producto</th>
                                <th class="text-center" scope="col">Sucursal</th>
                                <th class="text-center" scope="col">Stock</th>
                                <th class="text-center" scope="col">Acciones</th>
                            </tr>
                        </thead>
                        <tbody id="tbodyProductoSucursal"></tbody>
                    </table>
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
                <h2 class="fw-bold">No se encontró ningún producto, haga una búsqueda</h2>
                <img src="/Content/imagenes/BuscarProductos.png" alt="SinProductos" class="avatar-img m-5" width="25%" id="img">
            </div>
        </div>
    </div>`;
}

function GetStock(button) {
    const row = $(button).closest('tr');
    const idProductoSucursal = row.data('id');
    const originalStock = row.data('original-stock');
    // Cerrar cualquier edición previa
    $('tr[data-editing="true"]').each(function () {
        const editingId = $(this).data('id');
        CancelEdit($(`.buttonsActinos-${editingId} button`).first()[0]);
    });

    // Marcar esta fila como en edición
    row.attr('data-editing', 'true');

    ToggleBotonesEdit(idProductoSucursal, false);
    ToggleBotonesSaveCancel(idProductoSucursal, true);
    EnableInputToggle(idProductoSucursal, originalStock);
}

function ToggleBotonesEdit(idProductoSucursal, toggle) {
    let divButtonsEdit = $(`.buttonEdit-${idProductoSucursal}`);
    if (toggle) {
        divButtonsEdit.removeClass('d-none').addClass('d-block');
    } else {
        divButtonsEdit.removeClass('d-block').addClass('d-none');
    }
}

function ToggleBotonesSaveCancel(idProductoSucursal, toggle) {
    let divButtonsActions = $(`.buttonsActinos-${idProductoSucursal}`);
    if (toggle) {
        divButtonsActions.removeClass('d-none').addClass('d-block');
    } else {
        divButtonsActions.removeClass('d-block').addClass('d-none');
    }
}

function CancelEdit(button) {
    const row = $(button).closest('tr');
    const idProductoSucursal = row.data('id');
    const originalStock = row.data('original-stock');
    ToggleBotonesEdit(idProductoSucursal, true);
    ToggleBotonesSaveCancel(idProductoSucursal, false);
    RestaurarTd(idProductoSucursal, originalStock);
    row.removeAttr('data-editing');
}

function EnableInputToggle(idTd, originalStock) {
    let tdStock = $(`#editarStock-${idTd}`)[0];
    tdStock.innerHTML = `<i class="bi bi-boxes"></i> <input class="form-control" id="inptEditStock-${idTd}" onkeypress="SoloNumeros(event)" type="text" value="${originalStock}">`;
}

function RestaurarTd(idTd, originalStock) {
    let tdStock = $(`#editarStock-${idTd}`)[0];
    tdStock.innerHTML = `<i class="bi bi-boxes"></i> ${originalStock}`;
}

function ConfirmUpdateStock(button) {
    const row = $(button).closest('tr');
    const idProductoSucursal = row.data('id');
    const originalStock = row.data('original-stock');
    const nuevoStock = $(`#inptEditStock-${idProductoSucursal}`).val();

    if (nuevoStock > 0 && nuevoStock != originalStock) {
        let titulo = 'Stock';
        let mensaje = '¿Realmente está seguro de que quiere actualizar el stock?';
        let icono = 'warning';
        let textoConfirmacion = 'Sí, cambiar stock';
        let textoCancelar = 'Cancelar';

        MensajeConfirmacionTextoPropio(
            titulo,
            mensaje,
            textoConfirmacion,
            textoCancelar,
            icono,
            function () {
                Update(idProductoSucursal, nuevoStock, originalStock, row);
            },
            function () {
                CancelEdit(button);
            }
        );
    } else {
        AlertaModal('Stock no válido', 'El stock no puede ser menor que 0 ni igual al stock anterior', WarningALERT);
    }
}

function Update(idProductoSucursal, stockNuevo, stockAnterior, row) {
    var productoSucursal = CreateJsonStock(idProductoSucursal, stockNuevo, stockAnterior);

    $.ajax({
        url: urlUpdateStok,
        type: 'POST',
        dataType: 'JSON',
        data: productoSucursal,
        success: function (data) {
            if (data && data.Correct) {
                AlertaModal('Acción exitosa', 'El stock se actualizó correctamente', SuccessALERT);
                // Actualizamos el valor original en el data attribute
                row.data('original-stock', stockNuevo);
                RestaurarTd(idProductoSucursal, stockNuevo);
                ToggleBotonesEdit(idProductoSucursal, true);
                ToggleBotonesSaveCancel(idProductoSucursal, false);
                row.removeAttr('data-editing');
            } else {
                AlertaModal('Error al actualizar', data.ErrorMessage, ErrorALERT);
                RestaurarTd(idProductoSucursal, stockAnterior);
            }
        },
        error: function (xhr) {
            console.log(xhr);
            AlertaModal('Error al actualizar', `Error al actualizar el stock del producto. Código: ${xhr.status}`, ErrorALERT);
            RestaurarTd(idProductoSucursal, stockAnterior);
        }
    });
}

function CreateJsonStock(IdProductoSucursal, stockNuevo, stockAnterior) {
    return {
        "IdProductoSucursal": +IdProductoSucursal,
        "Stock": +stockNuevo,
        "Producto": {
            "IdProducto": +stockAnterior
        }
    };
}

function SoloNumeros(event) {
    var caracter = String.fromCharCode(event.which);
    if (!/[0-9]/.test(caracter)) {
        event.preventDefault();
        AlertaModal('Carácter no válido', 'El stock solo puede tener números enteros', WarningALERT);
    }
}