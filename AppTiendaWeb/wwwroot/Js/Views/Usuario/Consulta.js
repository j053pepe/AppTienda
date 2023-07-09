var UsuarioConsulta = {
    Init: () => {
        UsuarioConsulta.GetAll();

    },
    GetAll: () => {
        UsuarioServices.GetAll()
            .done(result => {
                if (result.data.lenght > 0)
                    result.data.map(usuario => {
                        $('#divListUser').append(CreateItemList(usuario));
                    });
                else
                    $('#divListUser').append('<li class="list-group-item"><label>Sin usuarios</label></li>');
            });
    },
    CreateItemList: (usuario) => {
        return `<a href="#" class="list-group-item list-group-item-action" data-usuarioId="${usuario.usuarioId}">` +
            '<div class="d-flex w-100 justify-content-between">' +
            `<h5 class="mb-1">${usuario.nombre} ${usuario.paterno} ${usuario.materno}</h5>` +
            `<small class="text-muted">Usuario ${usuario.activo ? 'Actiov' : 'Inactivo'}</small>` +
            '</div>' +
            `<p class="mb-1">${usuario.email}</p>` +
            `<p class="mb-1">${usuario.telefono}</p>` +
            '</a>';
    }
};

UsuarioConsulta.Init();