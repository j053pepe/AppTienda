using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.AppTiendaWebModels
{
    public class VentaReporteModelView
    {
        public int VentaId { get; set; }
        public int Cantidad { get;set; }
        public decimal Total { get; set; }
        public string FechaVenta { get; set; }
        public string NombreUsuario { get; set; }
        public bool Status { get; set; }
        public List<VentaReporteDetalleModelView> Productos { get; set; }
    }

    public class VentaReporteDetalleModelView
    {
        public int VentaDetalleId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public string NombreCodigo { get; set; }
    }
}
