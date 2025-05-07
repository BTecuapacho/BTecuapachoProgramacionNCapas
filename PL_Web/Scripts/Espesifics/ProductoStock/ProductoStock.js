var estatusEditarEstock = true

function GetAll() {
    var idSucursal = $('#ddlSucursal').val()
    if (idSucursal && idSucursal > 0) {
        $('#tbodyProductoSucursal').empty()
        $.ajax({
            url: `${urlGetAll}${idSucursal}`,
            type: 'GET',
            dataType: 'JSON',
            success: function (data) {
                var contenedor = $('#ContenidoStok')
                if (data.Correct) {
                    contenedor.empty()
                    if (data.objects != 0) {
                        contenedor.append(CuerpoGetAll())
                        $.each(data.Objects, function (contador, producto) {
                            producto.Contador = contador + 1
                            DrawRow(producto)
                        })
                    } else {
                        contenedor.append(SinResultados())
                    }
                } else {
                    AlertaModal('Error', 'Error al obtener los productos', ErrorALERT)
                }
            },
            error: function (xhr) {
                console.log(xhr)
                AlertaModal('Error', 'Error al obtener los productos', ErrorALERT)
            }
        })
    } else {
        $('#ContenidoStok').empty().append(SinResultados())
    }
}

function DrawRow(productoSucursal) {
    var src = productoSucursal.Producto.ImagenBase64 != "" && productoSucursal.Producto.ImagenBase64 != null ? "data:image/png;base64," + productoSucursal.Producto.ImagenBase64 : "/content/imagenes/SinIngredientes.png";
    var tr = `<tr>
                <td class="text-center">
                    ${productoSucursal.Contador}
                </td>
                <td class="text-center">
                    <div class="text-center mb-1">
                        <img style="width: 15%; filter: drop-shadow(4px 4px 4px rgba(0, 0, 0, 0.61)); " alt="Imagen ${productoSucursal.Producto.Nombre}" src="${src}" class="me-1 rounded-2 " />
                        ${productoSucursal.Producto.Nombre}
                    </div>
                </td>
                <td class="text-center">
                    <i class="bi bi-buildings"></i>
                    ${productoSucursal.Sucursal.Nombre}
                </td>
                <td class="text-center" id="editarStock-${productoSucursal.IdProductoSucursal}">
                    <i class="bi bi-boxes"></i>
                    ${productoSucursal.Stock}
                </td>
                <td class="text-center">
                    <div class="text-center buttonEdit-${productoSucursal.IdProductoSucursal}" d-block">
                        <button id="${productoSucursal.IdProductoSucursal}" name="editStock" class="rounded-circle btn btn-warning d-inline-flex align-items-center justify-content-center text-light m-1" onclick="GetStock(this)">
                            <i class="bi bi-pen-fill"></i>
                        </button>
                    </div>
                    <div class="text-center buttonsActinos-${productoSucursal.IdProductoSucursal} d-none">
                        <button name="cancelEditStok" class="rounded-circle btn btn-primary d-inline-flex align-items-center justify-content-center text-light m-1" onclick="">
                            <i class="bi bi-floppy2-fill"></i>
                        </button>
                        <button id="${productoSucursal.IdProductoSucursal}" class="rounded-circle btn btn-danger d-inline-flex align-items-center justify-content-center" onclick="CancelEdit(this)">
                            <i class="bi bi-x-square-fill"></i>
                        </button>
                    </div>
                </td>
            </tr>`
    $('#tbodyProductoSucursal').append(tr)
}

function CuerpoGetAll() {
    return `<div class="col-12">
                <div class="card shadow">
                    <div class="border-bottom title-part-padding">
                        <h4 class="card-title my-2 text-center">Lista de procutos</h4>
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
                                <tbody id="tbodyProductoSucursal">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>`
}

function SinResultados() {
    return `<div class="col-md-12">
                <div class="card p-5 border shadow-lg">
                    <div class="text-center">
                        <h2 class="fw-bold">No se encontró ningun producto, haga una busqueda</h2>
                        <img src="/Content/imagenes/BuscarProductos.png" alt="SinProsuctos" class="avatar-img m-5" width="25%" id="img">
                    </div>
                </div>
            </div>`
}

function GetStock(input) {
    //console.log(input.id)
    if (estatusEditarEstock) {
        estatusEditarEstock = false
        let idProductoSucursal = input.id
        let tdStock = $(`#editarStock-${input.id}`)[0]
        let stock = tdStock.innerText.trim()
        console.log(stock)
        //tdStock.innerHTML = ''
        ToggleBotonesEdit(idProductoSucursal, false)
        ToggleBotonesSaveCancel(idProductoSucursal, true)
    }
}

//El parametro toggle lo utilizo para saver si tengo que mostrar u ocultar el div
// True es Mostrar, False el Ocultar
function ToggleBotonesEdit(idProductoSucursal, toggle) {
    let divButttonsEdit = $(`.buttonEdit-${idProductoSucursal}`)
    if (toggle) {
        divButttonsEdit.removeClass('d-none');
        divButttonsEdit.removeClass('d-block');
        divButttonsEdit.addClass('d-block');

    } else {
        divButttonsEdit.removeClass('d-none');
        divButttonsEdit.removeClass('d-block');
        divButttonsEdit.addClass('d-none');
    }
    //let botones = $('[name="editStok"]');
    //$.each(botones, function (i, boton) {
    //    console.log(boton)
    //})
}

//El parametro toggle lo utilizo para saver si tengo que mostrar u ocultar el div
// True es Mostrar, False el Ocultar
function ToggleBotonesSaveCancel(idProductoSucursal, toggle) {
    let divButttonsEdit = $(`.buttonsActinos-${idProductoSucursal}`)
    if (toggle) {
        divButttonsEdit.removeClass('d-none');
        divButttonsEdit.removeClass('d-block');
        divButttonsEdit.addClass('d-block');
    } else {
        divButttonsEdit.removeClass('d-none');
        divButttonsEdit.removeClass('d-block');
        divButttonsEdit.addClass('d-none');
    }
    //let botones = $('[name="editStok"]');
    //$.each(botones, function (i, boton) {
    //    console.log(boton)
    //})
}


function CancelEdit(input) {
    let idProductoSucursal = input.id
    estatusEditarEstock = true
    ToggleBotonesEdit(idProductoSucursal, true)
    ToggleBotonesSaveCancel(idProductoSucursal, false)
}
