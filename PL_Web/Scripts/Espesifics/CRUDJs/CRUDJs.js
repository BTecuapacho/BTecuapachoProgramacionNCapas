var loader = new Loader('loader')
var confirmAlert = new ConfirmAlert('confirmAlert')
//mostrarMensajeToast('Ejemplo', SUCCESS_ALERT, 'Usuario ejemplo')
//mostrarMensajeToast('Ejemplo', WARNING_ALERT, 'Usuario ejemplo')
//mostrarMensajeToast('Ejemplo', DANGER_ALERT, 'Usuario ejemplo')
//mostrarMensajeToast('Ejemplo', INFO_ALERT, 'Usuario ejemplo')

$(document).ready(
    $("#datepicker").datepicker({
        dateFormat: "dd/mm/yy",
        showAnim: "slideDown"
    }),
    GetAll()
);

function OpenModal() {
    loader.setLoaderTitle('Cargando datos, por favor espere...')
    loader.setLoaderBody('Por favor espere en lo que se cargan los datos para insertar al nuevo usuario...')
    loader.openLoader()
    setTimeout(function () {
        loader.closeLoader()
        GetAllRoles()
        GetAllEstados()
        $('#ModalFrom').modal('show')
    }, 1000)
}

function CloseModal() {
    LimpiarFormulario()
    $('#ModalFrom').modal('hide')
}

function GetAll() {
    $.ajax({
        url: urlGetAll,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            if (result.Correct) {
                InstanciateTable(result.Objects)
            } else {
                $('#tbodyUsuarios').append(`<tr><td colspan="7"><div class="mt-2 alert alert-danger" role="alert">
                    No se encontraron registros
                </div></td></tr>`)
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}

function InstanciateTable(data) {
    $('#tbodyUsuarios').empty()
    var contador = 0
    $.each(data, function (i, usuario) {
        contador++;
        let imgSexo = usuario.Sexo == "HO" ? "usuarioMasculino.png" : "usuarioFemenino.png"
        let src = usuario.ImagenBase64 != '' ? `data:image/png;base64,${usuario.ImagenBase64}`: `/content/imagenes/${imgSexo}`
        let sexo = usuario.Sexo == "HO" ? '<span class="badge text-bg-primary"><i class="bi bi-person-standing"> Masculino</i></span>' : '<span class="badge text-bg-danger"><i class="bi bi-person-standing-dress"> Femenino</i></span>'
        let btnEstatus = usuario.Estatus ? `<a class="rounded-pill btn d-inline-flex align-items-center justify-content-center m-1">
                                            <div class="form-check form-switch">
                                                <input class="form-check-input" type="checkbox" id="${usuario.IdUsuario}" checked onchange="ConfirmarCambioEstatus(this)">
                                            </div>
                                        </a>` :
                                        `<a class="rounded-pill btn d-inline-flex align-items-center justify-content-center m-1">
                                            <div class="form-check form-switch">
                                                <input class="form-check-input" type="checkbox" id="${usuario.IdUsuario}" onchange="ConfirmarCambioEstatus(this)">
                                            </div>
                                        </a>`
        var tr = `<tr>
            <td class="text-center">
                ${contador}
            </td>
            <td class="text-center">
                <span class="badge text-bg-danger mb-1">
                    <i class="bi bi-upc"></i> ${usuario.CURP}
                </span>
                <div class="text-center mb-1">
                    <img style="width:15%" alt="Imagen ${usuario.Nombre} ${usuario.ApellidoPaterno} ${usuario.ApellidoMaterno}" src="${src}" class="me-1" />
                    ${usuario.Nombre} ${usuario.ApellidoPaterno} ${usuario.ApellidoMaterno}
                </div>
                <span class="badge badge text-bg-dark">
                    <i class="bi bi-person-badge"></i>
                    Rol: ${usuario.Rol.Nombre}
                </span>
                <span class="badge text-bg-secondary">
                    <i class="bi bi-person-badge-fill"></i>
                    Alias: ${usuario.UserName}
                </span>
            </td>
            <td class="text-center">
                <span class="badge text-bg-primary">
                    <i class="bi bi-envelope-at-fill"></i>
                    ${usuario.Email}
                </span><br />
                <span class="badge text-bg-success me-1">
                    <i class="bi bi-phone-fill"></i>
                    ${usuario.Celular}
                </span>
                <span class="badge text-bg-success">
                    <i class="bi bi-telephone-fill"></i>
                    ${usuario.Telefono}
                </span>
            </td>
            <td class="text-center">
                <i class="bi bi-cake-fill"></i> ${usuario.FechaNacimiento}
            </td>
            <td class="text-center">
                ${sexo}
            </td>
            <td class="text-center">
                <i class="bi bi-building-fill"></i>
                calle ${usuario.Direccion.Calle}, Int. #${usuario.Direccion.NumeroInterior}, Ext. #${usuario.Direccion.NumeroExterior}, colonia ${usuario.Direccion.Colonia.Nombre}, C.P. ${usuario.Direccion.Colonia.CodigoPostal},<br />
                ${usuario.Direccion.Colonia.Municipio.Nombre}, ${usuario.Direccion.Colonia.Municipio.Estado.Nombre}
            </td>
            <td class="text-center">
                ${btnEstatus}
                <button id="${usuario.IdUsuario}" name="editUsuario" class="rounded-circle btn btn-warning d-inline-flex align-items-center justify-content-center text-light m-1" onclick="GetById(this)">
                    <i class="bi bi-pen-fill"></i>
                </button>
                <button id="${usuario.IdUsuario}" class="rounded-circle btn btn-danger d-inline-flex align-items-center justify-content-center deleteUsuario" onclick="ConfirmarEliminacion(this)">
                    <i class="bi bi-trash-fill m-1"></i>
                </button>
            </td>
        </tr>`
        $('#tbodyUsuarios').append(tr)
        imgSexo = ''; src = ''; sexo = ''; estatus = ''; tr = '';
    })
}

function GetAllRoles() {
    $.ajax({
        url: urlGetAllRoles,
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            if (result.Correct) {
                let ddlRol = $('#ddlRol').empty().append('<option value="">Seleccione un rol</option>')
                $.each(result.Objects, function (i, valor) {
                    let opcion = '<option value="' + valor.IdRol + '">' + valor.Nombre + '</option>'
                    ddlRol.append(opcion)
                })
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}

function GetAllEstados() {
    $.ajax({
        url: urlGetAllEstados,
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            if (result.Correct) {
                let ddlEstado = $('#ddlEstado').empty().append('<option value="">Seleccione un estado</option>')
                $.each(result.Objects, function (i, valor) {
                    let opcion = '<option value="' + valor.IdEstado + '">' + valor.Nombre + '</option>'
                    ddlEstado.append(opcion)
                })
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}

function ConfirmarCambioEstatus(input) {
    let titulo = 'Estatus'
    let mensaje = 'Deseas cambiar el estatus del usuario'
    let icono = TipoAlertaModal(QuestionALERT)
    let textoConfirmacion = 'Si, cambiar estatus'
    let textoCancelar = 'Cancelar'
    MensajeConfirmacionTextoPropio(titulo, mensaje, textoConfirmacion, textoCancelar, icono, function () { CambiarEstatus(input) }, function () { cambiarCheckbox(input.id, !input.checked) })
}

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
            if (result.Correct) {
                AlertaModal('El estatus cambio', 'El estatus del usuario a cambiado', SuccessALERT)
                mostrarMensajeToast('El estatus del usuario a cambiado', SUCCESS_ALERT, 'El estatus cambio')
                cambiarCheckbox(input.id, input.checked)
            } else {
                AlertaModal('Error al actualizar', result.ErrorMessage, ErrorALERT)
                mostrarMensajeToast(result.ErrorMessage, DANGER_ALERT, 'Error al actualizar')
            }
        },
        error: function (xhr) {
            console.log(xhr)
            AlertaModal('Error al actualizar', `Error al cambiar el estatus del usuario. Código : ${xhr.status}`, ErrorALERT)
            mostrarMensajeToast(`Error al cambiar el estatus del usuario. Código : ${xhr.status}`, WARNING_ALERT, 'Error al actualizar')
        }
    })
}

function cambiarCheckbox(idInput, estatus) {
    $(`#${idInput}`).prop('checked', estatus)
}

function ConfirmarEliminacion(input) {
    var IdUsuario = input.id
    let titulo = 'Deseas eliminar este usuario'
    let mensaje = 'Esta acción es permanente y ya no será visible en los procesos internos del sistema'
    let icono = 'warning'
    let textoConfirmacion = 'Si, eliminar este usuario'
    let textoCancelar = 'Cancelar'
    MensajeConfirmacionTextoPropio(titulo, mensaje, textoConfirmacion, textoCancelar, icono, function () { deleteUsuario(IdUsuario) })
}

function deleteUsuario(IdUsuario) {
    loader.setLoaderTitle('Cargando, por favor espere...')
    loader.setLoaderBody('Por favor espere en lo que elimina al usuario...')
    loader.openLoader()
    setTimeout(function () {
        $.ajax({
            url: urlEliminarUsuario,
            data: { IdUsuario: IdUsuario },
            type: 'POST',
            dataType: 'JSON',
            success: function (result) {
                loader.closeLoader()
                if (result.Correct) {
                    GetAll()
                    AlertaModal('Usuario eliminado', 'El usuario se elimino de manera correcta', SuccessALERT)
                    mostrarMensajeToast('El usuario se elimino de manera correcta', SUCCESS_ALERT, 'Usuario eliminado')
                } else {
                    AlertaModal('¡Error!', result.ErrorMessage, ErrorALERT)
                    mostrarMensajeToast(result.ErrorMessage, DANGER_ALERT, 'Error al eliminar al usuario')
                }
            },
            error: function (xhr) {
                console.log(xhr)
                loader.closeLoader()
                AlertaModal('¡Error!', `Ocurrió un error al ejecutar la acción solicitada. Código : ${xhr.status}`, ErrorALERT)
                mostrarMensajeToast(`Error al eliminar el usuario. Código : ${xhr.status}`, WARNING_ALERT, 'Error al eliminar')
            }
        })
    }, 1000)
}

function LimpiarFormulario() {
    $('#IdUsuario').removeClass('is-invalid is-valid').val('')
    $('#IdDireccion').removeClass('is-invalid is-valid').val('')
    $('#Imagen').removeClass('is-invalid is-valid').val('')
    $('#Nombre').removeClass('is-invalid is-valid').val('')
    $('#ApellidoPaterno').removeClass('is-invalid is-valid').val('')
    $('#ApellidoMaterno').removeClass('is-invalid is-valid').val('')
    $('#Email').removeClass('is-invalid is-valid').val('')
    $('#password').removeClass('is-invalid is-valid').val('')
    $('#confirmPassword').removeClass('is-invalid is-valid').val('')
    $('#inptTelefono').removeClass('is-invalid is-valid').val('')
    $('#inptCelular').removeClass('is-invalid is-valid').val('')
    $('#datepicker').removeClass('is-invalid is-valid').val('')
    $('#inptCRUP').removeClass('is-invalid is-valid').val('')
    $('#ddlRol').removeClass('is-invalid is-valid').val('')
    $('#inptUserName').removeClass('is-invalid is-valid').val('')
    $('input[name="Sexo"][value="HO"]').removeClass('is-invalid is-valid')
    $('input[name="Sexo"][value="MU"]').removeClass('is-invalid is-valid')
    $('input[name="Sexo"][value="HO"]').prop('checked', false)
    $('input[name="Sexo"][value="MU"]').prop('checked', false)
    $('#Estatus').removeClass('is-invalid is-valid').prop('checked', false)
    $('#ddlEstado').removeClass('is-invalid is-valid').val('')
    $('#ddlMunicipio').removeClass('is-invalid is-valid').val('')
    $('#ddlColonia').removeClass('is-invalid is-valid').val('')
    $('#inptCalle').removeClass('is-invalid is-valid').val('')
    $('#NumeroInterior').removeClass('is-invalid is-valid').val('')
    $('#NumeroExterior').removeClass('is-invalid is-valid').val('')
    $('#inptImagen').removeClass('is-invalid is-valid').val('')
    var errorMessages = $('.error');
    $.each(errorMessages, function (i, errorMessage) {
        errorMessage.textContent = '';
        errorMessage.classList.remove('valid-feedback', 'invalid-feedback');
    })
    $('#imagenUsuario')[0].src = '/content/imagenes/usuarioMasculino.png';
}

function convertStringBase64(callback) {
    var input = $('#inptImagen')[0];
    if (input.files.length > 0) {
        var file = input.files[0];
        var reader = new FileReader();
        reader.onloadend = function () {
            var cadenaBase64 = reader.result;
            var cadenaBase64 = reader.result.split(',')[1];
            callback(cadenaBase64);
        }
        reader.readAsDataURL(file);
    } else {
        callback('');
    }
}

function CreateJsonUser(callback) {
    convertStringBase64(function (base64String) {
        var usuario = {
            "IdUsuario": $('#IdUsuario').val() || 0,
            "UserName": $('#inptUserName').val(),
            "Nombre": $('#Nombre').val(),
            "ApellidoPaterno": $('#ApellidoPaterno').val(),
            "ApellidoMaterno": $('#ApellidoMaterno').val(),
            "Email": $('#Email').val(),
            "FechaNacimiento": $('#datepicker').val(),
            "Password": $('#confirmPassword').val(),
            "Sexo": $('input[name="Sexo"]:checked').val(),
            "Telefono": $('#inptTelefono').val(),
            "Celular": $('#inptCelular').val(),
            "Estatus": $('#Estatus').prop('checked').toString(),
            "CURP": $('#inptCRUP').val(),
            "ImagenBase64": base64String || $('#Imagen').val(),
            "Rol": {
                "IdRol": $('#ddlRol').val()
            },
            "Direccion": {
                "IdDireccion": $('#IdDireccion').val() || 0,
                "Calle": $('#inptCalle').val(),
                "NumeroInterior": $('#NumeroInterior').val(),
                "NumeroExterior": $('#NumeroExterior').val(),
                "Colonia": {
                    "IdColonia": $('#ddlColonia').val(),
                    "Municipio": {
                        "IdMunicipio": $('#ddlMunicipio').val(),
                        "Estado": {
                            "IdEstado": $('#ddlEstado').val(),
                        }
                    }
                }
            }
        };
        if (typeof callback === 'function') {
            callback(usuario);
        }
        return usuario;
    });
}

function Add(usuario) {
    loader.setLoaderTitle('Guardando, por favor espere...');
    loader.setLoaderBody('Por favor espere en lo que se guarda el nuevo usuario...');
    loader.openLoader();
    setTimeout(function () {
        loader.closeLoader();
        $.ajax({
            url: urlAdd,
            data: JSON.stringify(usuario),
            type: 'POST',
            dataType: 'JSON',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.Correct) {
                    AlertaModal('Usuario registrado', 'El usuario se a registrado de manera correcta', SuccessALERT)
                    mostrarMensajeToast('El usuario se guardo de manera correcta', SUCCESS_ALERT, 'Usuario guardado')
                    GetAll();
                    CloseModal();
                } else {
                    AlertaModal('Error al registrar al usuario', result.ErrorMessage, ErrorALERT)
                    mostrarMensajeToast(result.ErrorMessage, DANGER_ALERT, 'Error al registrar al usuario')
                }
            },
            error: function (xhr) {
                console.log(xhr);
                AlertaModal('Error al registrar', `Error al insertar el usuario. Código : ${xhr.status}`, ErrorALERT)
                mostrarMensajeToast(`Error al insertar el usuario. Código : ${xhr.status}`, WARNING_ALERT, 'Error al registrar')
            }
        });
    }, 1000);
}

function Update(usuario) {
    loader.setLoaderTitle('Guardando, por favor espere...');
    loader.setLoaderBody('Por favor espere en lo que se guardan loa cambios del usuario...');
    loader.openLoader();
    setTimeout(function () {
        loader.closeLoader();
        $.ajax({
            url: urlUpdate,
            data: JSON.stringify(usuario),
            type: 'POST',
            dataType: 'JSON',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.Correct) {
                    AlertaModal('Usuario actualizado', 'El usuario se actualizo de manera correcta', SuccessALERT)
                    mostrarMensajeToast('El usuario se actualizo de manera correcta', SUCCESS_ALERT, 'Usuario actualizado')
                    GetAll();
                    CloseModal();
                } else {
                    AlertaModal('Error al actualizar al usuario', result.ErrorMessage, ErrorALERT)
                    mostrarMensajeToast(result.ErrorMessage, DANGER_ALERT, 'Error al actualizar al usuario')
                }
            },
            error: function (xhr) {
                console.log(xhr);
                AlertaModal('Error al actualizar', `Error al actualizar el usuario. Código : ${xhr.status}`, ErrorALERT)
                mostrarMensajeToast(`Error al actualizar el usuario. Código : ${xhr.status}`, WARNING_ALERT, 'Error al actualizar')
            }
        });
    }, 1000);
}

function Enviar() {
    if (!ValidateForm()) {
        mostrarMensajeToast('Por favor, complete todos los campos del formulario.', DANGER_ALERT, 'Formulario incompleto');
        AlertaModal('Formulario incompleto', 'Por favor, complete todos los campos del formulario.', ErrorALERT)
        return;
    }

    CreateJsonUser(function (usuario) {
        if (usuario.IdUsuario === 0) {
            Add(usuario);
        } else {
            Update(usuario);
        }
    })
}

function GetById(input) {
    $.ajax({
        url: `${urlGetById}${input.id}`,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            OpenModalEditar(result)
        },
        error: function (xhr) {
            console.log(xhr)
            alert(`Error al obtener el usuario. Código : ${xhr.status}`)
        }
    });
}

function llenarFormulario(usuario) {
    $('#IdUsuario').val(usuario.IdUsuario);
    $('#IdDireccion').val(usuario.Direccion.IdDireccion);
    $('#Imagen').val(usuario.ImagenBase64);
    $('#Nombre').val(usuario.Nombre);
    $('#ApellidoPaterno').val(usuario.ApellidoPaterno);
    $('#ApellidoMaterno').val(usuario.ApellidoMaterno);
    $('#Email').val(usuario.Email);
    $('#inptTelefono').val(usuario.Telefono);
    $('#inptCelular').val(usuario.Celular);
    $('#datepicker').val(usuario.FechaNacimiento);
    $('#inptCRUP').val(usuario.CURP);
    $('#inptUserName').val(usuario.UserName);

    $('#ddlRol').val(usuario.Rol.IdRol);
    if (usuario.Sexo === 'HO') {
        $('input[name="Sexo"][value="HO"]').prop('checked', true);
    }
    if (usuario.Sexo === 'MU') {
        $('input[name="Sexo"][value="MU"]').prop('checked', true);
    }
    $('#Estatus').prop('checked', usuario.Estatus);

    $('#ddlEstado').val(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
    $('#ddlMunicipio').val(usuario.Direccion.Colonia.Municipio.IdMunicipio);
    $('#ddlColonia').val(usuario.Direccion.Colonia.IdColonia);

    $('#inptCalle').val(usuario.Direccion.Calle);
    $('#NumeroInterior').val(usuario.Direccion.NumeroInterior);
    $('#NumeroExterior').val(usuario.Direccion.NumeroExterior);

    if (usuario.ImagenBase64) {
        $('#imagenUsuario').attr('src', `data:image/png;base64,${usuario.ImagenBase64}`);
    }
}

function OpenModalEditar(usuario) {
    loader.setLoaderTitle('Cargando datos, por favor espere...')
    loader.setLoaderBody('Por favor espere en lo que se cargan los datos del usuario')
    loader.openLoader()
    GetAllRoles()
    GetAllEstados()
    GetMunicipioByIdEstadoDraw(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
    GetColoniasByIdMunicipioDraw(usuario.Direccion.Colonia.Municipio.IdMunicipio);
    setTimeout(function () {
        loader.closeLoader()
        llenarFormulario(usuario)
        $('#ModalFrom').modal('show')
    }, 1000)
}

//===========================FROM VALIDATION===================
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
    var RegExpUserName = new RegExp(/^[a-zA-Z\s]+$/)
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
    $('.is-invalid').removeClass('is-invalid');
    $('.error').text('').removeClass('invalid-feedback');
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

    let sexoSeleccionado = $('input[name="Sexo"]:checked').val();
    if (!sexoSeleccionado) {
        $('input[name="Sexo"]').closest('.row').find('.error')
            .text('Debe seleccionar un sexo')
            .addClass('invalid-feedback');
        $('input[name="Sexo"]').addClass('is-invalid');
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

//==================AJAX DROPDOWNLISTS==================
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

function GetMunicipioByIdEstadoDraw(ddlEstado) {
    $.ajax({
        url: urlGetMunicipiosByIdEstado + ddlEstado,
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            if (result.Correct) {
                let ddlMunicipio = $('#ddlMunicipio')
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

function GetColoniasByIdMunicipioDraw(ddlMunicipio) {
    $.ajax({
        url: urlGetColoniasByIdMunicipio + ddlMunicipio,
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            if (result.Correct) {
                let ddlColonia = $('#ddlColonia')
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
}


//==================SECCION IMAGENES==================
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
