$(function () {
    var main = {
        events() {

            CallApi("get", "auth/Status")
                .then(result => {
                    console.log("Status", result);
                    LoginRegister();
                });
            LoginRegister = () => {
                $("div.error").hide();
                var ModalAuth = new bootstrap.Modal(document.getElementById('modalAuth'), {
                    keyboard: false
                }),
                    ModalRegister = new bootstrap.Modal(document.getElementById('modalRegister'), {
                        keyboard: false
                    });
                ModalAuth.toggle();
                $("#btnRegistro").on('click', () => {
                    ModalAuth.hide();
                    ModalRegister.show();
                });
                document.getElementById('modalRegister').addEventListener('hidden.bs.modal', function (event) {
                    ModalAuth.show();
                });

                CallEstadoApi.GetAll()
                    .done(result => {
                        SelectComponent.FillSelect("#EstadoId", ArrayComponent.DynamicArrayToSelectArray(result, "EstadoId", "Nombre"));
                    });
                $("#frmRegister").validate({
                    rules: {
                        Password: {
                            required: true
                        },
                        Password2: {
                            required: true,
                            equalTo: "#Password"
                        }
                    },
                    messages: {
                        Password: "Contraseña requerdia.",
                        Password2: "Por favor ingresa la misma contraseña."
                    },
                    invalidHandler: function (e, validator) {
                        var errors = validator.numberOfInvalids();
                        if (errors) {
                            var message = errors == 1 ? 'Te has saltado 1 campo. Han sido destacados acontinuacion' : 'Te has saltado ' + errors + ' campos.  Ellos han sido destacados acontinuacion';
                            $("div.error span").html(message);
                            $("div.error").show();
                        } else {
                            $("div.error").hide();
                        }
                    }
                });
                $('#frmRegister').on('submit', function (e) {
                    e.preventDefault();
                    const formData = document.getElementById("frmRegister");
                    if (formData.checkValidity()) {
                        let frmFormData = new FormData(formData);
                        frmFormData.append("Activo", true);
                        CallApiFormData("post", "auth/Register", frmFormData)
                            .done(result => {
                                console.log("Result", result);
                                if (result.statusCode == 200)
                                    alertify.alert('Usuario', 'Usuario creado con exito!', function () { alertify.success('Guardado'); });
                                else
                                    alertify.alert('Usuario', 'Error al crear el usuario!', function () { alertify.success(result.message); });
                            });
                    }
                });
            };
            GetData = () => {
                return {
                    Nombre: ""
                };
            }
        }
    };

    main.events();
});