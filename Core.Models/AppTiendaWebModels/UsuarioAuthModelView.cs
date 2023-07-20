using Core.Models.AppTiendaModels;
using Newtonsoft.Json;

namespace Core.Models.AppTiendaWebModels
{
    public class UsuarioAuthModelView
    {
        public UsuarioAuthModelView(Usuario user)
        {
            if (user != null)
            {
                this.UsuarioId = user.UsuarioId;
                this.UsuarioNombre = $"{user.Nombre} {user.ApellidoPaterno} {user.ApellidoMaterno}";
                this.UsuarioEmail = user.Email;
                this.UsuarioTime = DateTime.Now;
            }
        }

        public string UsuarioId { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioEmail { get; set; }
        public DateTime UsuarioTime { get; set; }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
