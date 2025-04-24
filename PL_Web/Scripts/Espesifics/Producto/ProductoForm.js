$(document).ready(
)

function Enviar(event) {
    if (!ValidateForm()) {
        event.preventDefault();
        alert('Por favor, complete todos los campos del formulario.')
    }
}

function SoloLetras(event) {
    var caracter = String.fromCharCode(event.which);
    var inputField = event.target;
    var errorMessage = inputField.parentNode.querySelector('.error');
    errorMessage.textContent = '';
    errorMessage.classList.remove('valid-feedback', 'invalid-feedback');
    if (/^[a-zA-Z\s]+$/.test(caracter)) {
        inputField.classList.remove('is-invalid');
        inputField.classList.add('is-valid');
        errorMessage.textContent = 'Entrada válida';
        errorMessage.classList.add('valid-feedback');
    } else {
        event.preventDefault();
        inputField.classList.remove('is-valid');
        inputField.classList.add('is-invalid');
        errorMessage.textContent = 'Solo se permiten letras';
        errorMessage.classList.add('invalid-feedback');
    }
}

function ValidateSelect(IdElemento) {
    var select = $('#' + IdElemento).val()
    var inputField = $('#' + IdElemento)[0]
    var errorMessage = inputField.parentNode.querySelector('.error')
    errorMessage.textContent = ''
    errorMessage.classList.remove('valid-feedback', 'invalid-feedback');
    if (select != '') {
        inputField.classList.remove('is-invalid');
        inputField.classList.add('is-valid');
        errorMessage.textContent = 'Opcion selecionada correctamente'
        errorMessage.classList.add('valid-feedback');
    } else {
        inputField.classList.remove('is-valid');
        inputField.classList.add('is-invalid');
        errorMessage.textContent = 'Debe selecionar un elemento'
        errorMessage.classList.add('invalid-feedback');
    }
}

function SoloNumeros(event) {
    var caracter = String.fromCharCode(event.which);
    var inputField = event.target;
    var errorMessage = inputField.parentNode.querySelector('.error');
    errorMessage.textContent = '';
    errorMessage.classList.remove('valid-feedback', 'invalid-feedback');

    if (/\d|\./.test(caracter)) {
        inputField.classList.remove('is-invalid');
        inputField.classList.add('is-valid');
        errorMessage.textContent = 'Entrada válida (carácter)';
        errorMessage.classList.add('valid-feedback');
        return true;
    } else {
        event.preventDefault();
        inputField.classList.remove('is-valid');
        inputField.classList.add('is-invalid');
        errorMessage.textContent = 'Solo se permiten números y el punto.';
        errorMessage.classList.add('invalid-feedback');
        return false;
    }
}

function validarNumeroCompleto(event) {
    var inputField = event.target;
    var value = inputField.value;
    var errorMessage = inputField.parentNode.querySelector('.error');
    errorMessage.textContent = '';
    errorMessage.classList.remove('valid-feedback', 'invalid-feedback');

    if (/^-?\d+(\.\d+)?$/.test(value) || value === "" || value === "-") {
        inputField.classList.remove('is-invalid');
        inputField.classList.add('is-valid');
        errorMessage.textContent = 'Número válido';
        errorMessage.classList.add('valid-feedback');
    } else {
        inputField.classList.remove('is-valid');
        inputField.classList.add('is-invalid');
        errorMessage.textContent = 'Formato de número inválido (ej: 3.5, -2, 10)';
        errorMessage.classList.add('invalid-feedback');
    }
}

function ValidateForm() {
    let isValid = true;

    function validateRequired(selector, errorMessage) {
        $(selector).not('#inptImagen').each(function () {
            let field = $(this);
            if (field.val() === '' || field.val() === null) {
                field.addClass('is-invalid');
                let errorDiv = field.parent().find('.error');
                errorDiv.text(errorMessage);
                errorDiv.addClass('invalid-feedback');
                console.log(field);
                isValid = false;
            } else {
                field.removeClass('is-invalid');
                field.removeClass('invalid-feedback');
                field.parent().find('.error').text(''); 
            }
        });
    }

    validateRequired('input[type="text"], textarea', 'Este campo es obligatorio');

    validateRequired('select', 'Debe seleccionar un elemento');

    let precioField = $('#Precio');
    let precioErrorMessage = precioField.parent().find('.error');
    let precioValue = parseFloat(precioField.val());

    if (!isNaN(precioValue) && precioValue <= 0) {
        precioField.addClass('is-invalid');
        precioErrorMessage.text('El precio no puede ser menor o igual a 0');
        precioErrorMessage.addClass('invalid-feedback');
        console.log("precio inválido");
        isValid = false;
    } else {
        precioField.removeClass('is-invalid');
        precioField.removeClass('invalid-feedback');
        precioErrorMessage.text('');
    }

    return isValid;
}

function GetSubCategoriaByIdSubCategoria() {
    let ddlCategoria = $('#ddlCategoria').val()
    if (ddlCategoria) {
        $.ajax({
            url: urlGetByIdCategoria + ddlCategoria,
            type: "GET",
            dataType: "JSON",
            success: function (result) {
                if (result.Correct) {
                    let ddlSubCategoria = $('#ddlSubCategoria').empty().append('<option value="">Seleccione una subcategoria</option>')
                    $.each(result.Objects, function (i, valor) {
                        let opcion = '<option value="' + valor.IdSubCategoria + '">' + valor.Nombre + '</option>'
                        ddlSubCategoria.append(opcion)
                    })
                }
            },
            error: function (xhr) {
                console.log(xhr)
            }
        })
    } else {
        $("#ddlSubCategoria").empty().append('<option value="">Seleccione una subcategoria</option>')
    }
}

function IsImage() {
    let archivo = $('#inptImagen')[0].files[0]
    let extencionArchivo = archivo.name.split('.').pop().toLowerCase();
    let contenIMG = $('#imagenProducto')[0]
    let extenciones = ["jpg", "jpeg", "png", "svg", "webp"]
    if (archivo.size > 1000000) {
        alert("El archivo no debe ser mayor a 1 MB.");
        $('#inptImagen').val('')
        contenIMG.src = '/content/imagenes/SinIngredientes.png'
        return
    }

    if ($.inArray(extencionArchivo, extenciones) === -1) {
        alert(`El archivo debe ser una imagen de tipo: ${extenciones}`)
        $('#inptImagen').val('')
        contenIMG.src = '/content/imagenes/SinIngredientes.png'
    } else {
        CambiarImg(archivo, contenIMG)
    }
}

function CambiarImg(input, contenIMG) {
    var reader = new FileReader()
    reader.onload = function (elemento) {
        contenIMG.src = elemento.target.result
    }
    reader.readAsDataURL(input)
}