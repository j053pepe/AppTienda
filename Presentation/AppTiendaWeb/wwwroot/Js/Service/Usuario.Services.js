var UsuarioServices = {
    Get: () => {
        return CallApi("get", "usuario/")
            .done(result => {
                return result;
            });
    },
    GetBasicData: () => {
        return CallApi("get", "usuario/BasicData")
            .done(result => {
                return result;
            });
    },
    GetAll: () => {
        return CallApi("get", "usuario/Usuarios")
            .done(result => {
                return result;
            });
    },
    Update: (data) => {
        return CallApi("put", "usuario/update", data)
            .done(result => {
                return result;
            });
    },
    ChangeStatus: (active, usuarioId) => {
        let type = active == "true" ? "delete" : "put";
        let url = active == "true" ? "usuario/delete/" : "usuario/activate/";
        url += usuarioId;
        return CallApi(type, url)
            .done(result => {
                return result;
            });
    }
};