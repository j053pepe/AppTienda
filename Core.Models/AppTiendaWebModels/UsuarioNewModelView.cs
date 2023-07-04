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
        public string Paterno { get; set; }
        [MinLength(3)]
        [MaxLength(100)]
        public string? Materno { get; set; }
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
        public bool Activo { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Calle { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Ciudad { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Colonia { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(6)]
        public string Cp { get; set; }
        public int EstadoId { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Numero { get; set; }
    }
}
