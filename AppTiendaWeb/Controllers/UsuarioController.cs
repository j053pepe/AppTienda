using Core.Contracts.Service;
using Core.Models.Enums;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.AppTiendaWeb.CustomAttributes;
using Core.Models.AppTiendaModels;
using Presentation.AppTiendaWeb.Helpers;

namespace Presentation.AppTiendaWeb.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [ValidateToken]
        public async Task<ActionResult<Usuario>> Get()
        {
            ModelResponse<Usuario> modelResponse = new();
            try
            {
                string token = Request.Headers["Token"];
                var userId = AesOperationHelper.DecryptString(token);
                var response = await _usuarioService.GetUsuarioById(userId);
                modelResponse.Data = response;
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
