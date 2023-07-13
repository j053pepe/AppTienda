using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.AppTiendaWebModels
{
    public class ProductoModelView
    {
        public int? ProductId { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public string Codigo { get;set; }

        [Required]
        public decimal Stock { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public IFormFile? ImagenProducto { get; set; }

        public string UrlImagen { get; set; }

    }
}
