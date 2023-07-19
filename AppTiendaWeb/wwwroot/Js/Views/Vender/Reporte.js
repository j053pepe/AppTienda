var ListVentas = [{
    ventaId: 0, cantidad: 0, total: 0.00, fechaVenta: "", nombreUsuario: "", status: true,
    productos: [{ ventaDetalleId: 0, cantidad: 0.00, precioUnitario: 0.00, total: 0.00, nombreCodigo: "", }]
}],
    moddalDetalle;

var VentaReporte = {
    init() {
        moddalDetalle = new bootstrap.Modal(document.getElementById('modalVentaDetalle'), {
            keyboard: false
        });
        this.GetVentas();
    },
    GetVentas() {
        ListVentas = [];
        jQuery("#tblVentas tbody").append('<tr><th scope="col" colspan="7" class="text-center">Sin registros</th></tr>');
        CallApi("get", `venta/reporte`)
            .done(result => {
                ListVentas = result.data;
                VentaReporte.DataToTable();
            })
            .fail(result => {
                alertify.alert('Reporte venta', 'Error al consultar!', function () {
                    alertify.error(result.message == undefined ? `${result.status}-${result.statusText}` : result.message);
                });
            });
    },
    DataToTable() {
        jQuery("#tblVentas tbody").empty();
        let totalVenta = 0;
        ListVentas.map(item => {
            let row = `<tr><th scope="row">${item.ventaId}</th>
            <td>${item.nombreUsuario}</td>
            <td>${item.fechaVenta}</td>
            <td class="text-center">${item.cantidad}</td>
            <td>${formatter.format(item.total)}</td>
            <td>${item.status ? 'Activo' : 'Inactivo'}</td>
            <td>
            <button class="badge text-bg-primary" data-button="Detalle">Detalle</button>
            <button class="badge text-bg-${item.status ? 'danger' : 'info'}" data-button="Status">${item.status ? 'Desactivar' : 'Activar'}</button>
            </td>
          </tr>`;
            jQuery("#tblVentas tbody").append(row);
            totalVenta += (item.status ? item.total : 0);
        });
        $('#lblTotalVenta')[0].innerText = formatter.format(totalVenta);
        $('*[data-button="Detalle"]').on('click', VentaReporte.ButtonDetalle);
        $('*[data-button="Status"]').on('click', VentaReporte.ButtonStatus);
    },
    ButtonDetalle() {
        let id = parseInt($($(this).closest('tr')[0].firstChild)[0].innerText);
        let item = ListVentas.find(x => x.ventaId == id);
        if (item != undefined)
            VentaReporte.FormModal(item);
    },
    ButtonStatus() {
        let id = parseInt($($(this).closest('tr')[0].firstChild)[0].innerText);
        CallApi("delete", `venta/${id}`)
            .done(result => {
                alertify.alert('Venta', 'Status de venta actualizado!', () => { location.reload(); });
            });
    },
    FormModal(venta) {
        $('#frmDetalle')[0].reset();
        moddalDetalle.show();
        $('#ventaId').val(venta.ventaId);
        $('#usuarioNombre').val(venta.nombreUsuario);
        $('#fechaVenta').val(venta.fechaVenta);
        $('#NoProductos').val(venta.cantidad);
        $('#totalVenta').val(formatter.format(venta.total));
        $('#StatusVenta').val(venta.status ? 'Activo' : 'Inactivo');
        VentaReporte.DetalleToTable(venta.productos);
    },
    DetalleToTable(listDetalle = []) {
        jQuery("#tblVentaDetalle tbody").empty();
        let totalVenta = 0;
        listDetalle.map(item => {
            let row = `<tr><td>${item.nombreCodigo}</td>
            <td>${formatter.format(item.precioUnitario)}</td>
            <td>${item.cantidad}</td>
            <td>${formatter.format(item.total)}</td>
          </tr>`;
            jQuery("#tblVentaDetalle tbody").append(row);
            totalVenta += item.total;
        });
        $('#lblTotalDetalleVenta')[0].innerText = formatter.format(totalVenta);
    }
};

VentaReporte.init();