using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.AppTiendaWebModels
{
    public class VentaModelView
    {
        public decimal Total { get; set; }
        public int NumeroProductos { get; set; }
        public List<VentaDetalleModelView> VentaDetalle { get; set; }
    }

    public class VentaDetalleModelView
    {
        public string Codigo { get; set; }
        public decimal Cantidad { get; set; }
    }
}
