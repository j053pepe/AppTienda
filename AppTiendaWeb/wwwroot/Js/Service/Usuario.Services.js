var UsuarioServices = {
    GetBasicData: () => {
        return CallApi("get","usuario/BasicData")
        .done(result=> {
            return result;
        });
    }
};