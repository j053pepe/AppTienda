var UsuarioServices = {
    Get: () => {
        return CallApi("get","usuario")
        .done(result=> {
            return result;
        });
    }
};