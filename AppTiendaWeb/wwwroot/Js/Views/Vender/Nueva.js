var VentaNueva = {
    ListProductos: [{ nombreCodigo: '', precio: 0, cantidad: 0, total: 0, codigo: '' }],
    init() {
        $('#NombreProducto').autocomplete();
        $('#btnNuevoProducto').on('click', this.AddNewItem);
        this.ListProductos = [];
        $('#frmVenta').on('submit', this.Vender);
    },
    Vender(e) {
        e.preventDefault();
        if (VentaNueva.ListProductos.length == 0) {
            alertify.alert('Venta', 'Ningun articulo agregado.');
            return false;
        } else {
            let dataApi = {
                NumeroProductos: VentaNueva.ListProductos.length,
                Total: $('#lblTotalVenta')[0].innerText.replace('$',''),
                VentaDetalle: VentaNueva.ListProductos
            };
            CallApi("post", `venta`, dataApi)
                .done(result => {
                    alertify.alert('Venta', result.data);
                    VentaNueva.ListProductos = [];
                    VentaNueva.ListToTable();
                })
                .fail(result => {
                    alertify.alert('Venta', 'Error al vender!', function () {
                        alertify.error(result.message == undefined ? `${result.status}-${result.statusText}` : result.message);
                    });
                });
        }
    },
    AddNewItem() {
        let productoSelect = $('#NombreProducto').val().split('|');
        if (productoSelect.length != 3)
            alertify.alert('Venta', 'Producto invalido.');
        else {
            if (parseFloat($('#CantidadProducto').val()) > 0) {
                var amount = parseFloat(productoSelect[2].replace('$', '')),
                    cantidad = parseFloat($('#CantidadProducto').val()),
                    nombreCodigo = `${productoSelect[0]} | ${productoSelect[1]}`;

                let itemIndex = VentaNueva.ListProductos.findIndex(x => x.codigo.trim().toLowerCase() == productoSelect[1].trim().toLowerCase());
                let itemVenta =
                {
                    cantidad: cantidad,
                    nombreCodigo: nombreCodigo,
                    codigo: productoSelect[1],
                    precio: productoSelect[2].replace('$',''),
                    total: cantidad * amount
                };

                if (itemIndex == -1)
                    VentaNueva.ListProductos.push(itemVenta);
                else
                    VentaNueva.ListProductos[itemIndex] = itemVenta;

                VentaNueva.ListToTable();
                $('#NombreProducto').val('');
                $('#CantidadProducto').val('');
            }
            else
                alertify.alert('Venta', 'Número de productos es necesario.');
        }
    },
    ListToTable() {
        jQuery("#tblVentaDetalle tbody").empty();
        let totalVenta = 0;
        VentaNueva.ListProductos.map(item => {
            let row = `<tr><td>${item.nombreCodigo}</td>
            <td>${formatter.format(item.precio)}</td>
            <td>${item.cantidad}</td>
            <td>${formatter.format(item.total)}</td>
            <td>
            <button class="badge text-bg-primary" data-button="Editar">Editar</button>
            <button class="badge text-bg-danger" data-button="Eliminar">Eliminar</button>
            </td>
          </tr>`;
            jQuery("#tblVentaDetalle tbody").append(row);
            totalVenta += item.total;
        });
        $('#lblTotalVenta')[0].innerText = formatter.format(totalVenta);
        $('*[data-button="Editar"]').on('click', VentaNueva.ButtonEditar);
        $('*[data-button="Eliminar"]').on('click', VentaNueva.ButtonStatus);
    },
    ButtonEditar() {
        console.log("ButtonEditar");
        let nameCode = $($(this).closest('tr')[0].firstChild)[0].innerText;
        let code = nameCode.split('|')[1];
        let item = VentaNueva.ListProductos.find(x => x.codigo.trim().toLowerCase() == code.trim().toLowerCase());
        if (item != undefined) {
            $('#NombreProducto').val(`${nameCode}|${item.precio}`);
            $('#CantidadProducto').val(item.cantidad);
        }
    },
    ButtonStatus() {
        console.log("ButtonStatus");
        let code = $($(this).closest('tr')[0].firstChild)[0].innerText.split('|')[1];
        let itemIndex = VentaNueva.ListProductos.findIndex(x => x.codigo.trim().toLowerCase() == code.trim().toLowerCase());
        if (itemIndex != -1) {
            VentaNueva.ListProductos.splice(itemIndex, 1);
            VentaNueva.ListToTable();
        }
    }
};

VentaNueva.init();