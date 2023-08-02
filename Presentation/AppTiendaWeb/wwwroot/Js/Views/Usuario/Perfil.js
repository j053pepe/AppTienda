var UsuarioPerfil = {
    main() {
        this.GetData();
        main.GetEstados("#PerfilEstadoId");
        $('#frmPerfil').on('submit', this.Update);
    },
    Update(e) {
        e.preventDefault();
        if ($('#PerfilPassword').val().length > 0 || $('#PerfilConfirmPassword').val().length > 0)
            if ($('#PerfilPassword').val() != $('#PerfilConfirmPassword').val()) {
                alertify.alert('Usuario', 'La contraseña debe coincidir con la confirmada!');
                return false;
            }

        UsuarioServices.Update(UsuarioPerfil.SetFormToData())
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
    GetData() {
        UsuarioServices.Get()
            .done(result => {
                UsuarioPerfil.SetDataToForm(result.data);
            });
    },
    SetDataToForm: (itemusuario) => {
        $('#PerfilUsuarioId').val(itemusuario.usuarioId);
        $('#PerfilNombre').val(itemusuario.nombre);
        $('#PerfilPaterno').val(itemusuario.paterno);
        $('#PerfilMaterno').val(itemusuario.materno);
        $('#PerfilTelefono').val(itemusuario.telefono);
        $('#PerfilEmail').val(itemusuario.email);
        $('#PerfilEstadoId').val(itemusuario.usuarioDetalle.estadoId);
        $('#PerfilCiudad').val(itemusuario.usuarioDetalle.ciudad);
        $('#PerfilCp').val(itemusuario.usuarioDetalle.cp);
        $('#PerfilColonia').val(itemusuario.usuarioDetalle.colonia);
        $('#PerfilCalle').val(itemusuario.usuarioDetalle.calle);
        $('#PerfilNumero').val(itemusuario.usuarioDetalle.numero);
        $('#PerfilActivo').val(itemusuario.activo);
    },
    SetFormToData: () => {
        let itemusuario = {
            usuarioId:$('#PerfilUsuarioId').val(),
            nombre: $('#PerfilNombre').val(),
            paterno: $('#PerfilPaterno').val(),
            materno: $('#PerfilMaterno').val(),
            telefono: $('#PerfilTelefono').val(),
            email: $('#PerfilEmail').val(),
            password: $('#PerfilPassword').val(),
            activo: $('#PerfilActivo').val() == 'true',
            usuarioDetalle: {
                estadoId: $('#PerfilEstadoId').val(),
                ciudad: $('#PerfilCiudad').val(),
                cp: $('#PerfilCp').val(),
                colonia: $('#PerfilColonia').val(),
                calle: $('#PerfilCalle').val(),
                numero: $('#PerfilNumero').val(),
            }
        };
        return itemusuario;
    },
};

UsuarioPerfil.main();