using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.AppTiendaWebModels
{
    public class TiendaNewModelView
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Direccion { get; set; }
        public IFormFile Imagen { get; set; }
    }
}