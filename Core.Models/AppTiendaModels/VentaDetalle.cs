using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Models.AppTiendaModels
{
    public partial class VentaDetalle
    {
        public int VentaDetalleId { get; set; }
        public int? VentaId { get; set; }
        public int? ProductoId { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Total { get; set; }

        public virtual Producto Producto { get; set; }
        public virtual Venta Venta { get; set; }
    }
}
