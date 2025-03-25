//corrige mi función MensajeConfirmacionTextoPropio para que ejecute la peticion ajax si preciona el boton de confirmación
//Tipos de alerts
var SuccessALERT = 1;
var ErrorALERT = 2;
var QuestionALERT = 3;
var WarningALERT = 4;
var InfoALERT = 5;
/* Función para realizar alertas en versión sweetalert, esta funcion recibe 3 parámetros
    título: Será el título principal del modal
    mensaje: Será el cuerpo que desplegará el modal
    tipo: 1 = success 2 = error, 3 = question, 4 = warning
*/
function AlertaModal(titulo = '', mensaje = '', tipo = 1) {
    Swal.fire(
        titulo,
        mensaje,
        TipoAlertaModal(tipo)
    );
} //end AlertaModal

/* función para saber que tipo de alerta es*/
function TipoAlertaModal(num) {
    tipo = '';
    switch (num) {
        case 1:
            tipo = 'success';
            break;
        case 2:
            tipo = 'error';
            break;
        case 3:
            tipo = 'question';
            break;
        case 4:
            tipo = 'warning';
            break;
        case 5:
            tipo = 'info';
            break;

        default:
            tipo = 'success';
            break;
    }//end switch
    return tipo;
} //end TipoAlertaModal


//====================================================
//FUNCIÓN PARA REALIZAR UNA CONFIRMACIÓN PERSONALIZADA
//====================================================
/*
* Función global para preguntar algo y luego realizar una acción
* Esta función funciona para realizar una petición al servidor pero antes de realizarlo preguntar.
* Esta función recibe 7 parámetros, los cuales son:
* titulo = Título para el mensaje que salga del SweetAlert
* body = Texto para el cuerpo del mensaje del sweetalert
* confirmText = Texto para el botón de confirmación
* cancelText = Texto para el botón de cancelación
* icon = El tipo de SweetAlert a solicitar, puede ser success, warning, question o danger.
* callback = una función a ejecutar.
* functionCancel = una función a ejecutar si no hay confirmación.
*/
function MensajeConfirmacionTextoPropio(titulo = '', mensaje = '', confirmText = '', cancelText = '', icon = 'warning', callback = null, functionCancel = null) {
    Swal.fire({
        title: titulo,
        text: mensaje,
        icon: icon,
        showCancelButton: true,
        confirmButtonColor: '#7bb13c',
        cancelButtonColor: '#e84646',
        confirmButtonText: confirmText,
        cancelButtonText: cancelText,
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            if (typeof callback === "function") {
                callback();
            }
        } else {
            if (typeof functionCancel === "function") {
                functionCancel();
            }
        }
    })
}//end MensajeConfirmacionTextoPropio