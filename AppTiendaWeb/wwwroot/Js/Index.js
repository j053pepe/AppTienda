﻿$(function () {
    var main = {
        init() {
            CallJson("data/Menu.json")
                .done(result => {
                    this.MenuJson = result;

                    if (window.location.hash.length == 0) {
                        this.MenuClick();
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
                    } else {
                        this.EventMenu(parseInt(window.location.hash.split('#')[1]));
                        this.DataUser();
                    }
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
                    if (result.data.storeExists) {
                        $('#lblNameStore').text(result.data.tiendaNombre);
                        $('#divImageTienda').css('background-image', 'url(' + result.data.imageLogo + ')');
                    }
                    else
                        alertify.alert("Alerta", "Es necesario dar de alta una tienda.", () => { main.CrearTienda(); });
                });
        },
        CrearTienda() {
            var ModalTiendaRegister = new bootstrap.Modal(document.getElementById('modalFirstTienda'), {
                keyboard: false
            });
            ModalTiendaRegister.toggle();
            ModalTiendaRegister.show();
            $('#frmFirstTienda').on('submit', function (e) {
                e.preventDefault();
                if ($('#ImagenTiendaNueva')[0].files.length > 0) {
                    let frmFormData = new FormData(document.getElementById("frmFirstTienda"));
                    frmFormData.append("imagenTienda", $('#ImagenTiendaNueva')[0].files[0]);
                    CallApiFormData("post", "tienda/register", frmFormData)
                        .done(result => {
                            if (result.statusCode == 200) {
                                ModalTiendaRegister.hide();
                                main.DataUser();
                            }
                            else
                                alertify.alert('Tienda', 'Error al crear la tienda!', function () { alertify.error(result.message); });
                        })
                        .fail(result => {
                            alertify.alert('Tienda', 'Error al crear la tienda!');
                        });
                }
                else
                    alertify.alert('Tienda', 'No ha seleccionado ninguna imagen!');
            });
        },
        MenuClick() {
            var itemMenu = document.querySelectorAll('a[name="linkMenu"]');

            itemMenu.forEach(x => {
                x.addEventListener('click', event => {
                    this.EventMenu(parseInt($(x)[0].href.split('#')[1]));
                });
            });
        },
        MenuJson: [{ MenuId: 0, View: "", Js: "" }],
        EventMenu(menuId) {
            let indexMenu = this.MenuJson.findIndex(x => x.MenuId == menuId);
            if (indexMenu != -1) {
                LoadContent(this.MenuJson[indexMenu].View+`?${new Date().getTime()}`)
                    .done(html => {
                        document.getElementById('divDynamic').innerHTML = html;
                        $("#divDynamic").append(`<script src=${this.MenuJson[indexMenu].Js}?${new Date().getTime()} type="text/javascript" type="text/javascript"></script>`);
                    });
            }
            else
                alertify.alert('Menu', 'Dirección url incorrecta!', () => { window.location = "/index.html"; });
        }
    };

    main.init();
});