using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.AppTiendaWebModels
{
    public class TiendaNewModelView
    {
        [Required]
        public string NombreTiendaNueva { get; set; }

        [Required]
        public string DescripcionTiendaNueva { get; set; }

        [Required]
        public string DireccionTiendaNueva { get; set; }

        [Required]
        public IFormFile ImagenTienda { get; set; }
    }

    public class TiendaEditModelView
    {
        [Required]
        public string NombreTienda{ get; set; }

        [Required]
        public string DescripcionTienda{ get; set; }

        [Required]
        public string DireccionTienda { get; set; }
        public IFormFile? ImagenTienda { get; set; }
    }
}