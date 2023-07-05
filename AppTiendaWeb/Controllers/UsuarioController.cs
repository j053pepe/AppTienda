using Core.Contracts.Service;
using Core.Models.Enums;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.AppTiendaWeb.CustomAttributes;
using Core.Models.AppTiendaModels;
using Presentation.AppTiendaWeb.Helpers;
using Core.Models.AppTiendaWebModels;

namespace Presentation.AppTiendaWeb.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITiendaService _tiendaService;
        public UsuarioController(IUsuarioService usuarioService, ITiendaService tiendaService)
        {
            _usuarioService = usuarioService;
            _tiendaService = tiendaService;
        }

        [HttpGet]
        [ValidateToken]
        [Route("BasicData")]
        public async Task<ActionResult<Usuario>> GetUserAndTienda()
        {
            ModelResponse<UsuarioTiendaView> modelResponse = new();
            try
            {
                string token = Request.Headers["Token"];
                var userId = AesOperationHelper.DecryptString(token);
                var responseUser = await _usuarioService.GetUsuarioById(userId);
                var responseStore = await _tiendaService.GetTienda();
                modelResponse.Data = new UsuarioTiendaView
                {
                    UsuarioNombre = $"{responseUser.Nombre} {responseUser.ApellidoPaterno} {responseUser.ApellidoMaterno}",
                    StoreExists = responseStore != null,
                    TiendaNombre = responseStore?.Nombre,
                };
            }
            catch (Exception ex)
            {
                modelResponse.Message = ex.Message;
                modelResponse.StatusCode = (int)EnumStatus.Error;
            }

            return Ok(modelResponse);
        }
    }
}
