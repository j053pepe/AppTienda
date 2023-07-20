using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.AppTiendaWebModels
{
    public class ProductoReporteModelView
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public decimal Precio { get; set; }
        public decimal Stock {get; set; }
        public decimal CantidadVendida { get; set; }
        public decimal VentaTotal { get; set; }
    }
}
