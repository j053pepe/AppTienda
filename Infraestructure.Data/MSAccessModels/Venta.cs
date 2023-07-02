using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Infraestructure.Data.MSAccessModels
{
    public partial class Venta
    {
        public Venta()
        {
            VentaDetalle = new HashSet<VentaDetalle>();
        }

        public int? VentaId { get; set; }
        public decimal? Total { get; set; }
        public int? NumeroPoductos { get; set; }
        public string UsuarioId { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? Activo { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<VentaDetalle> VentaDetalle { get; set; }
    }
}
