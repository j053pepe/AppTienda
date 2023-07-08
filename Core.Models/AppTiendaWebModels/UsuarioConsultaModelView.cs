namespace Core.Models.AppTiendaWebModels
{
    public class UsuarioConsultaModelView
    {
        public string UsuarioId { get; set; }
        public string Nombre {  get; set; }
        public string Paterno{ get; set; }
        public string Materno { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
        public string Fecha { get; set; }
    }
}
