using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Infraestructure.Data.MSAccessModels
{
    public partial class UsuarioDireccion
    {
        public string UsuarioId { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Colonia { get; set; }
        public string Cp { get; set; }
        public string Ciudad { get; set; }
        public int? EstadoId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
