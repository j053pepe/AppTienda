var VentaNueva = {
    ListProductos: [{ nombreCodigo: '', precio: 0, cantidad: 0, total: 0, codigo: '' }],
    init() {
        $('#NombreProducto').autocomplete();
        $('#btnNuevoProducto').on('click', this.AddNewItem);
        this.ListProductos=[];
    },
    AddNewItem() {
        console.log('AddNewItem');
        let productoSelect = $('#NombreProducto').val().split('|');
        var amount = parseFloat(productoSelect[2].replace('$', '')),
            cantidad = parseFloat($('#CantidadProducto').val());
        let itemVenta =
        {
            cantidad: cantidad,
            nombreCodigo: `${productoSelect[0]} | ${productoSelect[1]}`,
            codigo: productoSelect[1],
            precio: productoSelect[2],
            total: cantidad * amount
        };
        VentaNueva.ListProductos.push(itemVenta);
        VentaNueva.ListToTable();
    },
    ListToTable() {
        jQuery("#tblVentaDetalle tbody").empty();
        VentaNueva.ListProductos.map(item => {
            let row = `<tr><td>${item.nombreCodigo}</td>
            <td>${item.precio}</td>
            <td>${item.cantidad}</td>
            <td>${formatter.format(item.total)}</td>
            <td>
            <button class="badge text-bg-primary" data-button="Editar">Editar</button>
            <button class="badge text-bg-danger" data-button="Eliminar">Eliminar</button>
            </td>
          </tr>`;
            jQuery("#tblVentaDetalle tbody").append(row);
        });
        $('*[data-button="Editar"]').on('click', VentaNueva.ButtonEditar);
        $('*[data-button="Eliminar"]').on('click', VentaNueva.ButtonStatus);
    },
    ButtonEditar(){

    },
    ButtonStatus(){

    }
};

VentaNueva.init();