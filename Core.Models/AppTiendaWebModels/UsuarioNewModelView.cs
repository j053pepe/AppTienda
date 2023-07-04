using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.AppTiendaWebModels
{
    public class UsuarioNewModelView
    {
        [Required(ErrorMessage ="El nombre es requerido")]
        [MinLength(3, ErrorMessage ="Minimo 3 letras")]
        [MaxLength(100, ErrorMessage ="Maximo 100 caracteres")]
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
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(10)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,10}$", ErrorMessage = "La contraseña debe cumplir con los requisitos")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(10)]
        [Compare("Password")]
        [NotMapped]
        public string ConfirmPassword { get;set; }

        [MinLength(10)]
        [MaxLength(10)]
        public string Telefono { get;set; }

        [Required]
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

        [Required]
        public int EstadoId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Numero { get; set; }
    }
}
