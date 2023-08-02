var ListReporte = [{
    nombre: '',
    registros: [{ fecha: '', id: '', nombre: '',status:'' }]
}],
    moddalDetalle;

var UsuarioReporte = {
    init() {
        this.GetReporte();
    },
    GetReporte() {
        ListReporte = [];
        CallApi("get", `usuario/reporte`)
            .done(result => {
                ListReporte = result.data;
                UsuarioReporte.DataToTable();
            })
            .fail(result => {
                alertify.alert('Reporte venta', 'Error al consultar!', function () {
                    alertify.error(result.message == undefined ? `${result.status}-${result.statusText}` : result.message);
                });
            });
    },
    DataToTable() {
        jQuery("#accordionPanelsStayOpenExample").empty();
        ListReporte.map(item => {
            let rowDetail = '';
            item.registros.map((registro, index) => {
                rowDetail += `<div class="row border-bottom border-${index % 2 === 0 ? 'danger' : 'primary'}">
                <div class="col-md-2"><p>${registro.id}</p></div>
                    <div class="col-md-5"><p>${registro.nombre}</p></div>
                    <div class="col-md-3"><p>${registro.fecha}</p></div>
                    <div class="col-md-2"><p>${registro.status}</p></div>
                    </div>`;
            });
            let row = `<div class="accordion-item">
            <h2 class="accordion-header">
                <button class="accordion-button" type="button" data-bs-toggle="collapse"
                    data-bs-target="#div${item.nombre}" aria-expanded="true"
                    aria-controls="div${item.nombre}">
                    <p class="fw-semibold"> ${item.nombre}</p>
                </button>
            </h2>
            <div id="div${item.nombre}" class="accordion-collapse collapse show">
                <div class="accordion-body">
                <div class="row border-bottom border-black">
                <div class="col-md-2"><p class="fw-semibold">Id</p></div>
                    <div class="col-md-5"><p class="fw-semibold">Nombre</p></div>
                    <div class="col-md-3"><p class="fw-semibold">Fecha</p></div>
                    <div class="col-md-2"><p class="fw-semibold">Status</p></div>
                    </div>
                ${rowDetail}
            </div></div></div></div>`;
            jQuery("#accordionPanelsStayOpenExample").append(row);
        });
    }
};

UsuarioReporte.init();