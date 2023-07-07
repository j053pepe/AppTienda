using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.AppTiendaWebModels
{
    public class UsuarioTiendaView
    {
        public string UsuarioNombre { get;set; }
        public bool StoreExists { get;set; }
        public string TiendaNombre { get;set; } = string.Empty;
        public string ImageLogo {get;set; }= string.Empty;
    }
}
