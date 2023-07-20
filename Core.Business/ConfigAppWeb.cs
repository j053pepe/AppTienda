using Core.Models.AppTiendaModels;
using Core.Models.AppTiendaWebModels;

namespace Core.Business
{
    public class ConfigAppWeb
    {
        public Dictionary<string, string> Application = new Dictionary<string, string>();
        public UsuarioAuthModelView Usuario { get; set; }
        public string NewToken { get; set; }
    }
}
