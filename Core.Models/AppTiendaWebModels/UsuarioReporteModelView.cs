using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.AppTiendaWebModels
{
    public class UsuarioReporteModelView
    {
        public string Nombre { get; set; }
        public List<UsuarioReporteDetalle> Registros { get; set; }
    }

    public class UsuarioReporteDetalle
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Fecha { get; set; }
        public string Status { get; set; }
    }
}
