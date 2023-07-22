var UsuarioConsulta = {
    Init: () => {
        var ModalRegister = new bootstrap.Modal(document.getElementById('modalRegister'), {
            keyboard: false
        });
        UsuarioConsulta.GetAll();
        document.getElementById('modalRegister').addEventListener('hidden.bs.modal', function (event) {
            UsuarioConsulta.GetAll();
        });

        $('#btnNuevoUsuario').on('click', () => {
            main.ModalRegister(ModalRegister, UsuarioConsulta);
            $("#btnLogin").hide();
        });
        main.GetEstados("#UsuarioEstadoId");
        main.GetEstados("#EstadoId");
        $('#frmEditUsuario').on('submit', UsuarioConsulta.EditUsuario);
    },
    EditUsuario: (e) => {
        e.preventDefault();
        if ($('#UsuarioPassword').val().length > 0 || $('#UsuarioConfirmPassword').val().length > 0)
            if ($('#UsuarioPassword').val() != $('#UsuarioConfirmPassword').val()) {
                alertify.alert('Usuario', 'La contraseña debe coincidir con la confirmada!');
                return false;
            }

        UsuarioServices.Update(UsuarioConsulta.SetFormToData())
            .done(result => {
                alertify.success('Guardado');
                alertify.alert('Usuario', 'Usuario actualizado!', function () {
                    location.reload();
                });
            }).fail(result => {
                alertify.alert('Usuario', 'Error al actualizar al usuario!', function () {
                    alertify.error(result.message == undefined ? `${result.status}-${result.statusText}` : result.message);
                });
            });
    },
    Usuarios: [],
    GetAll: () => {
        $('#divListUser').empty();
        UsuarioServices.GetAll()
            .done(result => {
                if (result.data.length > 0) {
                    UsuarioConsulta.Usuarios = result.data;
                    result.data.map(usuario => {
                        $('#divListUser').append(UsuarioConsulta.CreateItemList(usuario));
                    });
                    UsuarioConsulta.EventUpdateStatus();
                    UsuarioConsulta.EventEditUsuario();
                }
                else
                    $('#divListUser').append('<li class="list-group-item"><label>Sin usuarios</label></li>');
            });
    },
    EventUpdateStatus: () => {
        let listBtnDesactivar = document.querySelectorAll('button[name="btnUsuarioDesactivar"]');

        listBtnDesactivar.forEach(x => {
            x.addEventListener('click', event => {
                let userActivo = x.getAttribute("data-activo");
                let userId = x.getAttribute("data-usuarioid");
                let messageConfirm = userActivo == "true" ? "¿Desea desactivar este usuario?" : "¿Desea activar este usuario?";
                alertify.confirm('Usuario', messageConfirm, function () {
                    UsuarioServices.ChangeStatus(userActivo, userId)
                        .done(result => {
                            alertify.success(result.data);
                            UsuarioConsulta.GetAll();
                        });
                }
                    , function () { alertify.error('Acción cancelada') });
            });
        });
    },
    EventEditUsuario: () => {
        ModalEdicion = new bootstrap.Modal(document.getElementById('modalUsuarioEdit'), {
            keyboard: false
        });
        let listBtnEdicion = document.querySelectorAll('button[name="btnUsuarioEditar"]');

        listBtnEdicion.forEach(x => {
            x.addEventListener('click', event => {
                let userId = x.getAttribute("data-usuarioid");
                let itemusuario = UsuarioConsulta.Usuarios.find(x => x.usuarioId == userId);
                ModalEdicion.show();
                UsuarioConsulta.SetDataToForm(itemusuario);
            });
        });
    },
    SetDataToForm: (itemusuario) => {
        $('#UsuarioNombre').val(itemusuario.nombre);
        $('#UsuarioPaterno').val(itemusuario.paterno);
        $('#UsuarioMaterno').val(itemusuario.materno);
        $('#UsuarioTelefono').val(itemusuario.telefono);
        $('#UsuarioEmail').val(itemusuario.email);
        $('#UsuarioEstadoId').val(itemusuario.usuarioDetalle.estadoId);
        $('#UsuarioCiudad').val(itemusuario.usuarioDetalle.ciudad);
        $('#UsuarioCp').val(itemusuario.usuarioDetalle.cp);
        $('#UsuarioColonia').val(itemusuario.usuarioDetalle.colonia);
        $('#UsuarioCalle').val(itemusuario.usuarioDetalle.calle);
        $('#UsuarioNumero').val(itemusuario.usuarioDetalle.numero);
        $('#EditUsuarioId').val(itemusuario.usuarioId);
        $('#EditUsuarioActivo').val(itemusuario.activo);
    },
    SetFormToData: () => {
        let itemusuario = {
            nombre: $('#UsuarioNombre').val(),
            paterno: $('#UsuarioPaterno').val(),
            materno: $('#UsuarioMaterno').val(),
            telefono: $('#UsuarioTelefono').val(),
            email: $('#UsuarioEmail').val(),
            password: $('#UsuarioPassword').val(),
            usuarioDetalle: {
                estadoId: $('#UsuarioEstadoId').val(),
                ciudad: $('#UsuarioCiudad').val(),
                cp: $('#UsuarioCp').val(),
                colonia: $('#UsuarioColonia').val(),
                calle: $('#UsuarioCalle').val(),
                numero: $('#UsuarioNumero').val(),
            },
            usuarioId: $('#EditUsuarioId').val(),
            activo: $('#EditUsuarioActivo').val() == "true"
        };
        return itemusuario;
    },
    CreateItemList: (usuario) => {
        let buttonStatus = usuario.activo ?
            `<button type="button" class="btn btn-outline-danger" name="btnUsuarioDesactivar" data-usuarioid="${usuario.usuarioId}" data-activo="${usuario.activo}">Desactivar</button>` :
            `<button type="button" class="btn btn-outline-primary" name="btnUsuarioDesactivar" data-usuarioid="${usuario.usuarioId}" data-activo="${usuario.activo}">Activar</button>`;
        let buttonEdicion = `<button type="button" name="btnUsuarioEditar" data-usuarioid="${usuario.usuarioId}" class="btn btn-outline-info">Editar</button>`;
        return '<div class="list-group list-group-horizontal">' +
            `<li class="list-group-item list-group-item-action">` +
            '<div class="d-flex w-100 justify-content-between">' +
            `<h5 class="mb-1">${usuario.nombre} ${usuario.paterno} ${usuario.materno}</h5>` +
            `<small class="text-muted fw-bold">${usuario.activo ? 'Activo' : 'Inactivo'}</small>` +
            '</div>' +
            `<p class="mb-1">${usuario.email}</p>` +
            `<p class="mb-1">${usuario.telefono}</p>` +
            '</li>' +
            '<li class="list-group-item">' +
            '<div class="d-grid gap-2">' +
            buttonEdicion + buttonStatus +
            '</div>' +
            '</li>' +
            '</div>';
    }
};

UsuarioConsulta.Init();