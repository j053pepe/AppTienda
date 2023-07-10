var UsuarioConsulta = {
    Init: () => {
        var ModalRegister = new bootstrap.Modal(document.getElementById('modalRegister'), {
            keyboard: false
        })
        UsuarioConsulta.GetAll();

        $('#btnNuevoUsuario').on('click', () => {
            main.ModalRegister(ModalRegister, UsuarioConsulta);
            $("#btnLogin").hide();
            $('#btnCloseRegister').on('click', () => {
                ModalRegister.hide();
            });
        });
    },
    GetAll: () => {
        $('#divListUser').empty();
        UsuarioServices.GetAll()
            .done(result => {
                console.table(result.data)
                if (result.data.length > 0) {
                    result.data.map(usuario => {
                        $('#divListUser').append(UsuarioConsulta.CreateItemList(usuario));
                    });
                    var listBtnDesactivar = document.querySelectorAll('button[name="btnUsuarioDesactivar"]');

                    listBtnDesactivar.forEach(x => {
                        x.addEventListener('click', event => {
                            let userActivo = x.getAttribute("data-activo");
                            let userId = x.getAttribute("data-usuarioid");
                            let messageConfirm = userActivo == "true" ? "¿Desea desactivar este usuario?" : "¿Desea activar este usuario?";
                            let type = userActivo == "true" ? "delete" : "put";
                            let url = userActivo == "true" ? "usuario/delete/" : "usuario/activate/";
                            url += userId;
                            alertify.confirm('Usuario', messageConfirm, function () {
                                CallApi(type, url)
                                    .done(result => {
                                        alertify.success(result.data);
                                        UsuarioConsulta.GetAll();
                                    });
                            }
                                , function () { alertify.error('Acción cancelada') });
                        });
                    });
                }
                else
                    $('#divListUser').append('<li class="list-group-item"><label>Sin usuarios</label></li>');
            });
    },
    CreateItemList: (usuario) => {
        let buttonStatus = usuario.activo ?
            `<button type="button" class="btn btn-outline-danger" name="btnUsuarioDesactivar" data-usuarioid="${usuario.usuarioId}" data-activo="${usuario.activo}">Desactivar</button>` :
            `<button type="button" class="btn btn-outline-primary" name="btnUsuarioDesactivar" data-usuarioid="${usuario.usuarioId}" data-activo="${usuario.activo}">Activar</button>`;
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
            '<button type="button" class="btn btn-outline-info">Editar</button>' +
            buttonStatus +
            '</div>' +
            '</li>' +
            '</div>';
    }
};

UsuarioConsulta.Init();