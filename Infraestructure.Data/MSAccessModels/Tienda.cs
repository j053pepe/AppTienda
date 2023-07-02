using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Infraestructure.Data.MSAccessModels
{
    public partial class Tienda
    {
        public int? TiendaId { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual TiendaDetalle TiendaDetalle { get; set; }
    }
}
