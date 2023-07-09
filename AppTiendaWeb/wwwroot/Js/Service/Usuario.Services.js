var UsuarioServices = {
    GetBasicData: () => {
        return CallApi("get","usuario/BasicData")
        .done(result=> {
            return result;
        });
    },
    GetAll:()=> {
        return CallApi("get","usuario/Usuarios")
        .done(result=> {
            return result;
        });
    }
};