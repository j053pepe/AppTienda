var myProductModal;
var ProductConsulta = {
    ListProduct: [{ codigo: '', descripcion: '', nombre: '', precio: 0, productId: 0, status: false, stock: '', urlImagen: '' }],
    init() {
        myProductModal = new bootstrap.Modal(document.getElementById('modalProducto'), {
            keyboard: false
        });
        this.GetProductos();
        $('#btnNuevoProducto').on('click', this.FormModal);
        ImageComponent.AddChangeWithPreview('#ImagenProducto', '#imgPreview');
        $('#frmProducto').on('submit', this.SubmitProducto);
    },
    ButtonEditar() {
        let id = parseInt($($(this).closest('tr')[0].firstChild)[0].innerText);
        let itemList = ProductConsulta.ListProduct.find(x => x.productId == id);
        if (itemList != undefined) {
            ProductConsulta.FormModal();
            ProductConsulta.DataToInput(itemList);
        }
    },
    ButtonStatus() {
        let id = parseInt($($(this).closest('tr')[0].firstChild)[0].innerText);
        ProductoServices.UpdateStatus(id)
            .done(result => {
                alertify.alert('Tienda', 'Status de producto Actualizado!', () => { location.reload(); });
            });
    },
    SubmitProducto(e) {
        e.preventDefault();
        let formData = ProductConsulta.InputToFormData();
        if ($('#ProductId').val() != '')
            ProductoServices.Update(formData, $('#ProductId').val())
                .done(result => {
                    if (result.statusCode != 500)
                        alertify.alert('Tienda', 'Producto Actualizado!', () => { location.reload(); });
                    else
                        alertify.alert('Tienda', result.message);
                });
        else
            ProductoServices.Insert(formData)
                .done(result => {
                    if (result.statusCode != 500)
                        alertify.alert('Tienda', 'Producto Creado!', () => { location.reload(); });
                    else
                        alertify.alert('Tienda', result.message);
                });
    },
    FormModal() {
        $('#frmProducto')[0].reset();
        $('#imgPreview').attr("src", "");
        myProductModal.show();
    },
    GetProductos() {
        jQuery("#tblProduct tbody").empty();
        jQuery("#tblProduct tbody").append('<tr><th scope="col" colspan="7" class="text-center">Sin registros</th></tr>');
        ProductoServices.GetAll()
            .done(result => {
                if (result.data.length > 0) {
                    this.ListProduct = result.data;
                    this.ListToTable();
                }
            });
    },
    ListToTable() {
        jQuery("#tblProduct tbody").empty();
        this.ListProduct.map(item => {
            let row = `<tr><th scope="row">${item.productId}</th>
            <td>${item.nombre}</td>
            <td>${item.precio}</td>
            <td>${item.codigo}</td>
            <td>${item.stock}</td>
            <td>${item.status ? 'Activo' : 'Inactivo'}</td>
            <td>
            <button class="badge text-bg-primary" data-button="Editar">Editar</button>
            <button class="badge text-bg-${item.status ? 'danger' : 'info'}" data-button="Status">${item.status ? 'Desactivar' : 'Activar'}</button>
            </td>
          </tr>`;
            jQuery("#tblProduct tbody").append(row);
        });
        $('*[data-button="Editar"]').on('click', this.ButtonEditar);
        $('*[data-button="Status"]').on('click', this.ButtonStatus);
    },
    InputToFormData() {
        var FormModal = new FormData();
        FormModal.append('ProductId', $('#ProductId').val());
        FormModal.append('Nombre', $('#NombreProducto').val());
        FormModal.append('Descripcion', $('#DescripcionProducto').val());
        FormModal.append('Codigo', $('#CodigoProducto').val());
        FormModal.append('Stock', $('#StockProducto').val());
        FormModal.append('Precio', $('#PrecioProducto').val());
        if ($('#ImagenProducto')[0].files.length > 0)
            FormModal.append("ImagenProducto", $('#ImagenProducto')[0].files[0]);
        if ($('#ProductId').val() != '')
            FormModal.append('Status', this.ListProduct.find(x => x.productId == parseInt($('#ProductId').val())).status);
        else
            FormModal.append('Status', true);

        return FormModal;
    },
    DataToInput(data) {
        $('#ProductId').val(data.productId);
        $('#NombreProducto').val(data.nombre);
        $('#DescripcionProducto').val(data.descripcion);
        $('#CodigoProducto').val(data.codigo);
        $('#StockProducto').val(data.stock);
        $('#PrecioProducto').val(data.precio);
        $('#imgPreview').attr("src", data.urlImagen);
    }
};

ProductConsulta.init();