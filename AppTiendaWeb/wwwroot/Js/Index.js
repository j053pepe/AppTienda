$(function () {
    var main = {
        init() {
            this.events();
            if (localStorage.getItem("token") == 'null' || localStorage.getItem("token") == null)
                LoginRegister();
            else
                CallApi("get", "auth/Status")
                    .then(result => {
                        if (!result.data) {
                            ModalAuth.hide();
                            ModalRegister.show();
                            $("#btnLogin").hide();
                        } else
                            this.DataUser();
                    });
        },
        events() {
            $('#btnSalir').on('click', () => {
                localStorage.removeItem("token");
            });
            LoginRegister = () => {
                var ModalAuth = new bootstrap.Modal(document.getElementById('modalAuth'), {
                    keyboard: false
                }),
                    ModalRegister = new bootstrap.Modal(document.getElementById('modalRegister'), {
                        keyboard: false
                    });
                ModalAuth.toggle();
                ModalAuth.show();
                $("#btnRegistro").on('click', () => {
                    ModalAuth.hide();
                    ModalRegister.show();
                });
                $("#btnLogin").on('click', () => {
                    ModalAuth.show();
                    ModalRegister.hide();
                });
                document.getElementById('modalRegister').addEventListener('hidden.bs.modal', function (event) {
                    ModalAuth.show();
                });

                CallEstadoApi.GetAll()
                    .done(result => {
                        SelectComponent.FillSelect("#EstadoId", ArrayComponent.DynamicArrayToSelectArray(result, "EstadoId", "Nombre"));
                    });
                $('#frmRegister').on('submit', function (e) {
                    e.preventDefault();
                    const formData = document.getElementById("frmRegister");
                    let frmFormData = new FormData(formData);
                    frmFormData.append("Activo", true);
                    CallApiFormData("post", "auth/Register", frmFormData)
                        .done(result => {
                            if (result.statusCode == 200)
                                alertify.alert('Usuario', 'Usuario creado con exito!', function () { alertify.success('Guardado'); });
                            else
                                alertify.alert('Usuario', 'Error al crear el usuario!', function () { alertify.error(result.message); });
                        })
                        .fail(result => {
                            alertify.alert('Usuario', 'Error al crear el usuario!', function () { alertify.error(result.message); });
                        });
                });
                $('#frmLogin').on('submit', function (e) {
                    e.preventDefault();
                    const formData = document.getElementById("frmLogin");
                    let frmFormData = new FormData(formData);
                    CallApi("post", "auth/login", Object.fromEntries(frmFormData))
                        .done(result => {
                            if (result.statusCode == 200) {
                                localStorage.setItem("token", result.data);
                                ModalAuth.hide();
                                main.DataUser();
                            }
                            else
                                alertify.alert('Usuario', 'Error al iniciar sesion!', function () { alertify.error(result.message); });
                        })
                        .fail(result => {
                            alertify.alert('Usuario', 'Error al iniciar sesion!');
                        });
                });
            };
        },
        DataUser() {
            UsuarioServices.GetBasicData()
                .done(result => {
                    $("#lblNameUser").text(result.data.usuarioNombre);
                    if (result.data.StoreExists)
                        $('#lblNameStore').text(result.data.tiendaNombre);
                    else
                    alertify.alert("Alerta","Es necesario dar de alta una tienda.",()=> {main.CrearTienda();}); 
                });
        },
        CrearTienda() {            
            var ModalTiendaRegister = new bootstrap.Modal(document.getElementById('modalFirstTienda'), {
                keyboard: false
            });
            ModalTiendaRegister.toggle();
            ModalTiendaRegister.show();
            //$('#modalFirstTienda')
        }
    };

    main.init();
});