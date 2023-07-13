var ProductConsulta = {
    ListProduct: [{ codigo: '', descripcion: '', nombre: '', precio: 0, productId: 0, status: false, stock: '', urlImagen: '' }],
    init() {
        this.GetProductos();
        $('#btnNuevoProducto').on('click', this.FormModal);
        ImageComponent.AddChangeWithPreview('#ImagenProducto','#imgPreview');
        $('#frmProducto').on('submit',this.SubmitProducto);
    },
    SubmitProducto(e){
        e.preventDefault();
    },
    FormModal(){
        $('#frmProducto')[0].reset();
        const myProductModal = new bootstrap.Modal(document.getElementById('modalProducto'), {
            keyboard: false
        });
        myProductModal.show();
    },
    GetProductos() {
        $('#tblProductBody').empty();
        jQuery("#tblProduct tbody").append('<tr><th scope="col" colspan="7" class="text-center">Sin registros</th></tr>');
        CallApi("get", "producto", "")
            .done(result => {
                if (result.data.length > 0) {
                    this.ListProduct = result.data;
                    this.ListToTable();
                }                
            }).fail(result => {
                alertify.alert('Producto', 'Error al consultar!', function () {
                    alertify.error(result.message == undefined ? `${result.status}-${result.statusText}` : result.message);
                });
            });
    },
    ListToTable() {
        $('#tblProductBody').empty();
        this.ListProduct.map(item => {
            let row = `<tr><th scope="row">${item.productId}</th>
            <td>${item.nombre}</td>
            <td>${item.precio}</td>
            <td>${item.codigo}</td>
            <td>${item.stock}</td>
            <td>${item.status ? 'Activo' : 'Inactivo'}</td>
            <td>
            <button class="badge rounded-pill text-bg-primary">Editar</button>
            <button class="badge rounded-pill text-bg-${item.status?'danger':'info'}">${item.status? 'Desactivar' : 'Activar'}</button>
            </td>
          </tr>`;
          jQuery("#tblProduct tbody").append(row);
        });
    }
};

ProductConsulta.init();