$(function () {
    var main = {
        events() {

            CallApi("get", "auth/Status")
                .then(result => {
                    console.log("Status", result);
                    LoginRegister();
                });

            LoginRegister = () => {
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
                    ModalRegister.toggle();
                });
                document.getElementById('modalRegister').addEventListener('hidden.bs.modal', function (event) {
                    ModalAuth.show();
                    ModalAuth.toggle();
                });
            }
        }
    };

    main.events();
});