using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Models.AppTiendaModels
{
    public partial class Usuario
    {
        public Usuario()
        {
            Producto = new HashSet<Producto>();
            Tienda = new HashSet<Tienda>();
            Venta = new HashSet<Venta>();
        }

        public string UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }
        public bool? Activo { get; set; }

        public virtual UsuarioDireccion UsuarioDireccion { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
        public virtual ICollection<Tienda> Tienda { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
