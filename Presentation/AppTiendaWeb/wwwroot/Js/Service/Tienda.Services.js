var TiendaServices = {
    GetBasicData: () => {
        return CallApi("get", "Tienda/BasicData")
            .done(result => {
                return result;
            });
    },
    UpdateTienda: (formData, id) => {
        return CallApiFormData("put", "Tienda/Actualizar/" + id, formData)
            .done(result => {
                return result.data;
            });
    }
};