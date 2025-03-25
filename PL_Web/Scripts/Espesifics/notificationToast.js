//Tipos de alerts
var SUCCESS_ALERT = 1;
var DANGER_ALERT = 2;
var INFO_ALERT = 3;
var WARNING_ALERT = 4;

const mostrarMensajeToast = (texto = "", tipo_mensaje = 5, titulo = "", tiempo = 3000) => {
    let configuracion_toast = {
        "closeButton": true,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "showDuration": "100",
        "hideDuration": "100",
        "timeOut": tiempo,
        "extendedTimeOut": "100",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "slideDown",
        "hideMethod": "fadeOut"
    };

    switch (tipo_mensaje) {
        case 1:
            toastr.success(texto, titulo, configuracion_toast);
            break;
        case 2:
            toastr.error(texto, titulo, configuracion_toast);
            break;
        case 3:
            toastr.info(texto, titulo, configuracion_toast);
            break;
        case 4:
            toastr.warning(texto, titulo, configuracion_toast);
            break;
        default:
            toastr.info(texto, titulo, configuracion_toast);
            break;
    }
};