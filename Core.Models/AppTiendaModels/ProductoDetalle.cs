using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Models.AppTiendaModels
{
    public partial class ProductoDetalle
    {
        public int ProductoId { get; set; }
        public string Descripcion { get; set; }
        public string UrlImage { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
