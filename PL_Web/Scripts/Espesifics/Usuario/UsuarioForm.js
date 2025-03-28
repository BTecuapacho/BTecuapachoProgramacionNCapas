$(document).ready(
    $("#datepicker").datepicker({
        dateFormat: "dd/mm/yy",
        showAnim: "slideDown"
    })
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

function SoloEmail() {
    var regExp = new RegExp(/^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$/)
    var inputField = $('#Email')[0]
    var errorMessage = inputField.parentNode.querySelector('.error')
    errorMessage.textContent = ''
    errorMessage.classList.remove('valid-feedback', 'invalid-feedback');
    var inEmail = $('#Email').val()
    if (regExp.test(inEmail)) {
        inputField.classList.remove('is-invalid');
        inputField.classList.add('is-valid');
        errorMessage.textContent = 'El formato del correo es correcto'
        errorMessage.classList.add('valid-feedback');
    } else {
        inputField.classList.remove('is-valid');
        inputField.classList.add('is-invalid');
        errorMessage.textContent = 'El formato del correo es incorrecto'
        errorMessage.classList.add('invalid-feedback');
    }
}

function ValidatePassword() {
    var password = $('#password').val();
    var passwordField = $('#password')[0]
    var passwordError = passwordField.parentNode.querySelector('.error')
    var passwordRegExp = new RegExp(/^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$/)
    passwordError.textContent = ''
    passwordError.classList.remove('valid-feedback', 'invalid-feedback')
    if (passwordRegExp.test(password)) {
        passwordField.classList.remove('is-invalid');
        passwordField.classList.add('is-valid')
        passwordError.textContent = 'La contraseña es válida.'
        passwordError.classList.add('valid-feedback')
    } else {
        passwordField.classList.remove('is-valid');
        passwordField.classList.add('is-invalid')
        passwordError.textContent = 'La contraseña debe tener mínimo 8 caracteres, una mayúscula, una minúscula, un número y un carácter especial.'
        passwordError.classList.add('invalid-feedback')
    }
}

function ValidatePasswordConfirm() {
    var password = $('#password').val();
    var confirmPassword = $('#confirmPassword').val()
    var confirmPasswordField = $('#confirmPassword')[0]
    var confirmPasswordError = confirmPasswordField.parentNode.querySelector('.error')
    confirmPasswordError.textContent = ''
    confirmPasswordError.classList.remove('valid-feedback', 'invalid-feedback')
    if (confirmPassword === password && password !== '') {
        confirmPasswordField.classList.remove('is-invalid');
        confirmPasswordField.classList.add('is-valid')
        confirmPasswordError.textContent = 'Las contraseñas coinciden.'
        confirmPasswordError.classList.add('valid-feedback')
    } else {
        confirmPasswordField.classList.remove('is-valid');
        confirmPasswordField.classList.add('is-invalid')
        confirmPasswordError.textContent = 'Las contraseñas no coinciden.'
        confirmPasswordError.classList.add('invalid-feedback')
    }
}

function ValidateTelefono(IdElemento) {
    var RegExpTelefono = new RegExp(/^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$/)
    var inputField = $('#' + IdElemento)[0]
    var errorMessage = inputField.parentNode.querySelector('.error')
    errorMessage.textContent = ''
    errorMessage.classList.remove('valid-feedback', 'invalid-feedback');
    var elemento = $('#' + IdElemento).val()
    if (RegExpTelefono.test(elemento)) {
        inputField.classList.remove('is-invalid');
        inputField.classList.add('is-valid');
        errorMessage.textContent = 'El formato telefinico es correcto'
        errorMessage.classList.add('valid-feedback');
    } else {
        inputField.classList.remove('is-valid');
        inputField.classList.add('is-invalid');
        errorMessage.textContent = 'El formato telefinico es incorrecto'
        errorMessage.classList.add('invalid-feedback');
    }
}

function ValidateFecha() {
    var RegExpFecha = new RegExp(/\d{1,2}\/\d{1,2}\/\d{2,4}/)
    var inputField = $('#datepicker')[0]
    var errorMessage = inputField.parentNode.querySelector('.error')
    errorMessage.textContent = ''
    errorMessage.classList.remove('valid-feedback', 'invalid-feedback');
    var fecha = $('#datepicker').val()
    if (RegExpFecha.test(fecha)) {
        inputField.classList.remove('is-invalid');
        inputField.classList.add('is-valid');
        errorMessage.textContent = 'El formato de la fecha es correcto'
        errorMessage.classList.add('valid-feedback');
    } else {
        inputField.classList.remove('is-valid');
        inputField.classList.add('is-invalid');
        errorMessage.textContent = 'El formato de la fecha es incorrecto'
        errorMessage.classList.add('invalid-feedback');
    }
}

function ValidateCURP() {
    var RegExpCURP = new RegExp(/^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/)
    var inputField = $('#inptCRUP')[0]
    var errorMessage = inputField.parentNode.querySelector('.error')
    errorMessage.textContent = ''
    errorMessage.classList.remove('valid-feedback', 'invalid-feedback');
    var curp = $('#inptCRUP').val()
    if (RegExpCURP.test(curp)) {
        inputField.classList.remove('is-invalid');
        inputField.classList.add('is-valid');
        errorMessage.textContent = 'El formato del curp es correcto'
        errorMessage.classList.add('valid-feedback');
    } else {
        inputField.classList.remove('is-valid');
        inputField.classList.add('is-invalid');
        errorMessage.textContent = 'El formato del curp es incorrecto'
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

function ValidateUserName() {
    var RegExpUserName = new RegExp(/^[a-zA-Z0-9](_(?!(\.|_))|\.(?!(_|\.))|[a-zA-Z0-9]){6,18}[a-zA-Z0-9]$/)
    var inputField = $('#inptUserName')[0]
    var errorMessage = inputField.parentNode.querySelector('.error')
    errorMessage.textContent = ''
    errorMessage.classList.remove('valid-feedback', 'invalid-feedback');
    var userName = $('#inptUserName').val()
    if (RegExpUserName.test(userName)) {
        inputField.classList.remove('is-invalid');
        inputField.classList.add('is-valid');
        errorMessage.textContent = 'El nombre de usuario es valido'
        errorMessage.classList.add('valid-feedback');
    } else {
        inputField.classList.remove('is-valid');
        inputField.classList.add('is-invalid');
        errorMessage.textContent = 'Nombre de usuario no valido'
        errorMessage.classList.add('invalid-feedback');
    }
}

function ValidateAlphanumericoEspacios() {
    var RegExpCalle = new RegExp(/^[a-zA-Z0-9 ]+$/)
    var inputField = $('#inptCalle')[0]
    var errorMessage = inputField.parentNode.querySelector('.error')
    errorMessage.textContent = ''
    errorMessage.classList.remove('valid-feedback', 'invalid-feedback');
    var userName = $('#inptCalle').val()
    if (RegExpCalle.test(userName)) {
        inputField.classList.remove('is-invalid');
        inputField.classList.add('is-valid');
        errorMessage.textContent = 'El nombre de la calle es valido'
        errorMessage.classList.add('valid-feedback');
    } else {
        inputField.classList.remove('is-valid');
        inputField.classList.add('is-invalid');
        errorMessage.textContent = 'Nombre de calle no valido'
        errorMessage.classList.add('invalid-feedback');
    }
}

function SoloNumeros(event) {
    var caracter = String.fromCharCode(event.which);
    var inputField = event.target;
    var errorMessage = inputField.parentNode.querySelector('.error');
    errorMessage.textContent = '';
    errorMessage.classList.remove('valid-feedback', 'invalid-feedback');
    if (/[0-9]/.test(caracter)) {
        inputField.classList.remove('is-invalid');
        inputField.classList.add('is-valid');
        errorMessage.textContent = 'Entrada válida';
        errorMessage.classList.add('valid-feedback');
    } else {
        event.preventDefault();
        inputField.classList.remove('is-valid');
        inputField.classList.add('is-invalid');
        errorMessage.textContent = 'Solo se permiten números';
        errorMessage.classList.add('invalid-feedback');
    }
}

function ValidateForm() {
    let isValid = true;
    $('input[type="text"], input[type="password"], select').not('#inptImagen').each(function () {
        let input = $(this);
        if (input.attr('id') !== 'Estatus') {
            if (input.val() === '' || input.val() === null) {
                input.addClass('is-invalid');
                let errorDiv = input.parent().find('.error');
                errorDiv.text('Este campo es obligatorio');
                errorDiv.addClass('invalid-feedback');
                isValid = false;
            }
        }
    });

    if (!$('input[name="Sexo"]:checked').val()) {
        $('input[name="Sexo"]').parent().parent().addClass('is-invalid');
        isValid = false;
    }

    $('select').each(function () {
        let select = $(this);
        if (select.val() === '' || select.val() === null) {
            select.addClass('is-invalid');
            let errorDiv = select.parent().find('.error');
            errorDiv.text('Debe seleccionar un elemento');
            errorDiv.addClass('invalid-feedback');
            isValid = false;
        }
    });

    let password = $('#password').val();
    let confirmPassword = $('#confirmPassword').val();
    if (password !== confirmPassword) {
        $('#confirmPassword').addClass('is-invalid');
        $('#confirmPassword').parent().find('.error')
            .text('Las contraseñas no coinciden')
            .addClass('invalid-feedback');
        isValid = false;
    }
    return isValid;
}

function GetMunicipioByIdEstado() {
    let ddlEstado = $("#ddlEstado").val()
    if (ddlEstado) {
        $.ajax({
            url: urlGetMunicipiosByIdEstado + ddlEstado,
            type: "GET",
            dataType: "JSON",
            success: function (result) {
                if (result.Correct) {
                    let ddlMunicipio = $('#ddlMunicipio').empty().append('<option value="">Seleccione un Municipio</option>')
                    $("#ddlColonia").empty().append('<option value="">Seleccione una colonia</option>')
                    $.each(result.Objects, function (i, valor) {
                        let opcion = '<option value="' + valor.IdMunicipio + '">' + valor.Nombre + '</option>'
                        ddlMunicipio.append(opcion)
                    })
                }
            },
            error: function (xhr) {
                console.log(xhr)
            }
        })
    } else {
        $("#ddlMunicipio").empty().append('<option value="">Seleccione un Municipio</option>')
        $("#ddlColonia").empty().append('<option value="">Seleccione una colonia</option>')
    }
}

function GetColoniasByIdMunicipio() {
    let ddlMunicipio = $('#ddlMunicipio').val()
    if (ddlMunicipio) {
        $.ajax({
            url: urlGetColoniasByIdMunicipio + ddlMunicipio,
            type: "GET",
            dataType: "JSON",
            success: function (result) {
                if (result.Correct) {
                    let ddlColonia = $('#ddlColonia').empty().append('<option value="">Seleccione una colonia</option>')
                    $.each(result.Objects, function (i, valor) {
                        let opcion = '<option value="' + valor.IdColonia + '">' + valor.Nombre + '</option>'
                        ddlColonia.append(opcion)
                    })
                }
            },
            error: function (xhr) {
                console.log(xhr)
            }
        })
    } else {
        $("#ddlColonia").empty().append('<option value="">Seleccione una colonia</option>')
    }
}

function IsImage() {
    let archivo = $('#inptImagen')[0].files[0]
    let extencionArchivo = archivo.name.split('.').pop().toLowerCase();
    let contenIMG = $('#imagenUsuario')[0]
    let extenciones = ["jpg", "jpeg", "png", "svg", "webp"]
    if (archivo.size > 1000000) {
        alert("El archivo no debe ser mayor a 1 MB.");
        $('#inptImagen').val('')
        contenIMG.src = '/content/imagenes/usuarioMasculino.png'
        return
    }

    if ($.inArray(extencionArchivo, extenciones) === -1) {
        alert(`El archivo debe ser una imagen de tipo: ${extenciones}`)
        $('#inptImagen').val('')
        contenIMG.src = '/content/imagenes/usuarioMasculino.png'
    } else {
        CambiarImg(archivo, contenIMG)
        //contenIMG.src = URL.createObjectURL(archivo)
    }
}

function CambiarImg(input, contenIMG) {
    var reader = new FileReader()
    reader.onload = function (elemento) {
        contenIMG.src = elemento.target.result
    }
    reader.readAsDataURL(input)
}

function CambiarImgSexo() {
    if ($('#inptImagen').val() === '') {
        let inptSexo = $('input[name="Sexo"]:checked').val()
        let contenIMG = $('#imagenUsuario')[0]
        contenIMG.src = (inptSexo === 'HO' ? '/content/imagenes/usuarioMasculino.png' : '/content/imagenes/usuarioFemenino.png')
    }
}