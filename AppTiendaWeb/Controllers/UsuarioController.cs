using Core.Contracts.Service;
using Core.Models.Enums;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.AppTiendaWeb.CustomAttributes;
using Core.Models.AppTiendaModels;
using Presentation.AppTiendaWeb.Helpers;
using Core.Models.AppTiendaWebModels;
using Core.Business;
using System.Text.Json;

namespace Presentation.AppTiendaWeb.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITiendaService _tiendaService;
        private readonly ConfigAppWeb _config;
        public UsuarioController(IUsuarioService usuarioService, ITiendaService tiendaService, ConfigAppWeb config)
        {
            _usuarioService = usuarioService;
            _tiendaService = tiendaService;
            _config = config;
        }

        [HttpGet]
        [ValidateToken]
        [Route("BasicData")]
        public async Task<ActionResult> GetUserAndTienda()
        {
            ModelResponse<UsuarioTiendaView> modelResponse = new();
            try
            {
                var responseStore = await _tiendaService.GetTienda();
                modelResponse.Data = new UsuarioTiendaView
                {
                    UsuarioNombre = $"{_config.Usuario.Nombre} {_config.Usuario.ApellidoPaterno} {_config.Usuario.ApellidoMaterno}",
                    StoreExists = responseStore != null,
                    TiendaNombre = responseStore?.Nombre ?? "",
                    ImageLogo = (responseStore?.TiendaDetalle?.UrlImage ?? "").Replace("wwwroot", "")
                };
            }
            catch (Exception ex)
            {
                modelResponse.Message = ex.Message;
                modelResponse.StatusCode = (int)EnumStatus.Error;
            }

            return Ok(modelResponse);
        }

        [HttpGet]
        [ValidateToken]
        [Route("Usuarios")]
        public async Task<ActionResult> GetAllUsers()
        {
            ModelResponse<List<UsuarioConsultaModelView>> response = new();
            try
            {
                var listUser = await _usuarioService.GetAll();
                response.Data = listUser.Where(x => x.UsuarioId != _config.Usuario.UsuarioId)
                    .Select(x => UsuarioHelper.UsuarioToUsuarioConsultaModelView(x))
                    .ToList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.StatusCode = (int)EnumStatus.Error;
            }

            return Ok(response);
        }
    }
}
