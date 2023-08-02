var TiendaConsulta = {
    init: () => {
        TiendaConsulta.GetTienda();
        ImageComponent.AddChangeWithPreview('#ImagenTienda', '#imgPreview');
        $('#frmTiendaEdit').on('submit', TiendaConsulta.UpdateTienda);
    },
    GetTienda: () => {
        TiendaServices.GetBasicData()
            .done(result => {
                TiendaConsulta.DataToInput(result.data);
            });
    },
    DataToInput(result) {
        $('#TiendaId').val(result.tiendaId);
        $('#NombreTienda').val(result.nombre);
        $('#DescripcionTienda').val(result.descripcion);
        $('#DireccionTienda').val(result.direccion);
        //$('#TelefonoTienda').val(result.telefono);
        //$('#EmailTienda').val(result.email);
        $('#imgPreview').attr("src", result.urlImage);
    },
    UpdateTienda: (e) => {
        e.preventDefault();
        let formData = new FormData();
        formData.append("NombreTienda", $('#NombreTienda').val());
        formData.append("DescripcionTienda", $('#DescripcionTienda').val());
        formData.append("DireccionTienda", $('#DireccionTienda').val());
        if ($('#ImagenTienda')[0].files.length > 0)
            formData.append("ImagenTienda", $('#ImagenTienda')[0].files[0]);

        TiendaServices.UpdateTienda(formData, $('#TiendaId').val())
            .done(result => {
                alertify.alert('Tienda', 'Tienda Actualizada!', () => { location.reload(); });
            });
    }
};

TiendaConsulta.init();