﻿using System.ComponentModel.DataAnnotations;

namespace Core.Models.AppTiendaWebModels
{
    public class UsuarioLoginModelView
    {
        [Required(ErrorMessage ="Ingrese una direccion de correo valida.")]
        [EmailAddress]
        public string EmailLogin { get; set; }
        [Required(ErrorMessage ="Ingrese una contraseña valida.")]
        [MinLength(6)]
        public string PasswordLogin { get; set; }
    }
}
