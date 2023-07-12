var TiendaConsulta = {
    init: () => {
        TiendaConsulta.GetTienda();
        $('#ImagenTienda').on("change", TiendaConsulta.ReadFile);
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
    ReadFile: () => {
        let file = $('#ImagenTienda')[0].files;
        if (!file || !file[0]) return;

        const FR = new FileReader();
        FR.addEventListener("load", function (evt) {
            $('#imgPreview').attr("src", evt.target.result);
        });

        FR.readAsDataURL(file[0]);
    },
    UpdateTienda: (e) => {
        e.preventDefault();
        let formData = new FormData();
        formData.append("NombreTienda", $('#NombreTienda').val());
        formData.append("DescripcionTienda", $('#DescripcionTienda').val());
        formData.append("DireccionTienda", $('#DireccionTienda').val());
        if ($('#ImagenTienda')[0].files.length>0)
            formData.append("ImagenTienda", $('#ImagenTienda')[0].files[0]);

            TiendaServices.UpdateTienda(formData, $('#TiendaId').val())
            .done(result => {
                alertify.alert('Tienda', 'Tienda Actualizada!', () => { location.reload(); });
            });
    }
};

TiendaConsulta.init();