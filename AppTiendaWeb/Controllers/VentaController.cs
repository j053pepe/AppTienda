using Core.Business;
using Core.Contracts.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.AppTiendaWeb.Controllers
{
    [Route("api/Producto")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        readonly IVentaService _ventaService;
        private readonly ConfigAppWeb _config;
        public VentaController(IVentaService ventaService, ConfigAppWeb config)
        {
            _ventaService = ventaService;
            _config = config;
        }
    }
}
