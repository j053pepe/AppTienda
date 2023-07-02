using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Infraestructure.Data.MSAccessModels
{
    public partial class Producto
    {
        public Producto()
        {
            VentaDetalle = new HashSet<VentaDetalle>();
        }

        public int? ProductoId { get; set; }
        public string Nombre { get; set; }
        public decimal? Precio { get; set; }
        public int? Codigo { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Stock { get; set; }
        public string UsuarioId { get; set; }
        public bool? Activo { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ProductoDetalle ProductoDetalle { get; set; }
        public virtual ICollection<VentaDetalle> VentaDetalle { get; set; }
    }
}
