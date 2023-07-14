var ProductoServices = {
    GetAll(){
        return CallApi("get", "producto")
        .done(result => {
            return result.data;
        }).fail(result => {
            alertify.alert('Producto', 'Error al consultar!', function () {
                alertify.error(result.message == undefined ? `${result.status}-${result.statusText}` : result.message);
            });
        });
    },
    Insert(formData){
        return CallApiFormData("post", "Producto", formData)
        .done(result => {
            return result.data;
        })
        .fail(result => {
            alertify.alert('Producto', 'Error al crear el producto!', function () {
                alertify.error(result.message == undefined ? `${result.status}-${result.statusText}` : result.message);
            });
        });
    },
    Update(formData, id){
        return CallApiFormData("put", "Tienda/" + id, formData)
        .done(result => {
            return result.data;
        })
        .fail(result => {
            alertify.alert('Producto', 'Error al actualizar el producto!', function () {
                alertify.error(result.message == undefined ? `${result.status}-${result.statusText}` : result.message);
            });
        });
    }
};