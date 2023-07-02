using System.ComponentModel.DataAnnotations;

namespace Core.Models.AppTiendaWebModels
{
    public class UsuarioNewModelView
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string ApellidoPaterno { get; set; }
        [MinLength(3)]
        [MaxLength(100)]
        public string? ApellidoMaterno { get; set; }
        [Required]
        [EmailAddress]
        [MinLength(3)]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(10)]
        public string Password { get; set; }
        [MinLength(10)]
        [MaxLength(10)]
        public string Telefono { get;set; }

    }
}
