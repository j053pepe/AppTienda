using Core.Models.AppTiendaModels;

namespace Core.Business
{
    public class ConfigAppWeb
    {
        public Dictionary<string, string> Application = new Dictionary<string, string>();
        public Usuario Usuario { get; set; }
    }
}
