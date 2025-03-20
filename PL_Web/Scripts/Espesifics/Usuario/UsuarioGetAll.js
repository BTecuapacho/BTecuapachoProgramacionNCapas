$('#ModalGetAllExel').modal()
//============================================
// FUNCIÓN PARA INSTANCIAR UNA DATATABLE CON AJAX
//============================================
/*
Función global para instanciar una DataTable con AJAX y mejorar la experiencia de usuario.
Esta función recibe seis argumentos:
id_tabla -> El ID del elemento HTML donde se instanciará la DataTable.
ruta_del_json -> La URL del recurso que proveerá los datos en formato JSON.
tipo_peticion -> El tipo de petición HTTP a realizar (por defecto es 'GET').
bodyAjax -> Los datos que se enviarán con la petición AJAX.
elementos_columna -> Las definiciones de las columnas de la DataTable.
orden_columnas -> El orden de las columnas en la DataTable.
*/
//const DatatablePorUrl = (id_tabla = 'datatable', ruta_del_json = '', tipo_peticion = 'GET', bodyAjax, elementos_columna, orden_columnas) => {
//    let datatable = $('#' + id_tabla).DataTable({
//        'processing': true,
//        'scrollX': true,
//        'ajax': {
//            url: ruta_del_json,
//            data: bodyAjax,
//            type: tipo_peticion,
//        },
//        "columnDefs": elementos_columna,
//        drawCallback: function (settings) {
//            feather.replace();
//            let tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
//            let tooltipList = tooltipTriggerList.map(function(element){
//                return new bootstrap.Tooltip(element);
//            });
//        },
//        "columns": orden_columnas,
//        language: {
//            url: '//cdn.datatables.net/plug-ins/2.2.2/i18n/es-ES.json',
//            "paginate": {
//                "first": '<i class="bi bi-chevron-bar-left"></i>',
//                "last": '<i class="bi bi-chevron-bar-right"></i>',
//                "next": '<i class="bi bi-chevron-right"></i>',
//                "previous": '<i class="bi bi-chevron-left"></i>'
//            },
//        },
//        "sSearch": 'Buscar <i class="bi bi- search"></i> '
//    });
//    return datatable;
//}; //end Datatable_por_url

// Variable para la dataTable
let tableGetAllExecel = null;

function CambiarEstatus(input) {
    $.ajax({
        url: urlCambiarEstatus,
        dataType: "json",
        type: "POST",
        data: {
            IdUsuario: input.id,
            Estatus: input.checked
        },
        success: function (result) {
            result.Correct ? alert("El estatus del usuario a cambiado") : alert(result.ErrorMessage)
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}

function Mostrar() {
    $('#searchPanel').slideDown(700)
    $('#btnShow').remove()
    var btnOcultar = $('<button class= "btn btn-primary" id = "btnHide" onclick = "Ocultar()"><i class="bi bi-eye-slash-fill"></i> Ocultar Panel de Busqueda</button >')
    $('#actionButtons').append(btnOcultar)
}

function Ocultar() {
    $('#searchPanel').slideUp(700)
    $('#btnHide').remove()
    var btnMostrar = $('<button class= "btn btn-primary" id = "btnShow" onclick = "Mostrar()"><i class="bi bi-eye-fill"></i> Mostrar Panel de Busqueda</button>')
    $('#actionButtons').append(btnMostrar)
}

function MostrarExel() {
    $('#UploadExel').slideDown(700)
    $('#btnShowExel').remove()
    var btnMostrar = $('<button class= "btn btn-primary" id = "btnHideExel" onclick = "OcultarExel()"><i class="bi bi-filetype-xlsx"></i> Ocultar Panel Exel</button>')
    $('#exelButtons').append(btnMostrar)
}

function OcultarExel() {
    $('#UploadExel').slideUp(700)
    $('#btnHideExel').remove()
    var btnMostrar = $('<button class= "btn btn-primary" id = "btnShowExel" onclick = "MostrarExel()"><i class="bi bi-filetype-xlsx"></i> Mostrar Panel Exel</button>')
    $('#exelButtons').append(btnMostrar)
}

function ValidateExel() {
    var archivo = $('#inptExel')[0].files[0]
    var extencionArchivo = archivo.name.split('.').pop().toLowerCase()
    if (extencionArchivo != 'xlsx') {
        alert('El archivo debe ser un exel con extencion .xlsx')
        $('#inptExel').val('')
    }
}

function GetAllExcel() {
    $.ajax({
        url: urlGetAllExel,
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            if (result.Correct) {
                //console.log(result.Objects)
                InitDataTable(result.Objects)
            }
        },
        error: function (xhr) {
            console.Log(xhr)
        }
    })

    //const createGetAllExceltable = () => {
    //    columnsElements = [
    //        {
    //            "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15],
    //            "createdCell": function (td, cellData, rowData, row, col) {
    //                $(td).addClass("special-cell text-center");
    //            }
    //        }];
    //    columnsOrder = [
    //        { data: 'result.Objects.UserName' },
    //        { data: 'result.Objects.Nombre' },
    //        { data: 'result.Objects.ApellidoPaterno' },
    //        { data: 'result.Objects.ApellidoMaterno' },
    //        { data: 'result.Objects.Email' },
    //        { data: 'result.Objects.FechaNacimiento' },
    //        { data: 'result.Objects.Sexo' },
    //        { data: 'result.Objects.Telefono' },
    //        { data: 'result.Objects.Celular' },
    //        { data: 'result.Objects.Estatus' },
    //        { data: 'result.Objects.CURP' },
    //        { data: 'result.Objects.Rol.IdRol' },
    //        { data: 'result.Objects.Direccion.Calle' },
    //        { data: 'result.Objects.Direccion.NumeroInterior' },
    //        { data: 'result.Objects.Direccion.NumeroExterior' },
    //        { data: 'result.Objects.Direccion.Colonia.IdColonia' }
    //    ];
    //    return DatatablePorUrl('tablaExel', urlGetAllExel, 'GET', null, columnsElements, columnsOrder);
    //};//end createGetAllExceltable

    //tableGetAllExecel = createGetAllExceltable();
    }

function InitDataTable(data) {
    console.log(data)
    $('#tablaExel').DataTable({
        destroy: true,
        data: data,
        columns: [
            { data: 'UserName' },
            { data: 'Nombre' },
            { data: 'ApellidoPaterno' },
            { data: 'ApellidoMaterno' },
            { data: 'Email' },
            { data: 'FechaNacimiento' },
            { data: 'Sexo' },
            { data: 'Telefono' },
            { data: 'Celular' },
            { data: 'Estatus' },
            { data: 'CURP' },
            { data: 'Rol.IdRol' },
            { data: 'Direccion.Calle' },
            { data: 'Direccion.NumeroInterior' },
            { data: 'Direccion.NumeroExterior' },
            { data: 'Direccion.Colonia.IdColonia' }
        ]
    })
}

function OpenModalGetAllExcel() {
    GetAllExcel()
    setTimeout(function () {
        $('#ModalGetAllExel').modal('show');
    }, 500);
}

function CloseModalGetAllExcel() {
    $('#ModalGetAllExel').modal('hide')
}

$(document).ready(
    $('#usuarios').DataTable({
        layout: {
            topStart: {
                buttons: [
                    {
                        extend: 'pdf',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5]
                        }
                    },
                    {
                        extend: 'excel',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5]
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5]
                        }
                    },
                    'colvis'
                ]
            }
        },
        language: {
            url: '//cdn.datatables.net/plug-ins/2.2.2/i18n/es-ES.json',
            "paginate": {
                "first": '<i class="bi bi-chevron-bar-left"></i>',
                "last": '<i class="bi bi-chevron-bar-right"></i>',
                "next": '<i class="bi bi-chevron-right"></i>',
                "previous": '<i class="bi bi-chevron-left"></i>'
            },
        }
    })
)

