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
            ModelResponse<UsuarioTiendaView> modelResponse = new(_config.NewToken);
            try
            {
                var responseStore = await _tiendaService.GetTienda();
                modelResponse.Data = new UsuarioTiendaView
                {
                    UsuarioNombre = _config.Usuario.UsuarioNombre,
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
            ModelResponse<List<UsuarioConsultaModelView>> response = new(_config.NewToken);
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

        [HttpDelete]
        [ValidateToken]
        [Route("Delete/{usuarioId}")]
        public async Task<ActionResult> DeleteUsuario(string usuarioId)
        {
            ModelResponse<string> response = new(_config.NewToken);
            try
            {
                await _usuarioService.UpdateStatus(usuarioId);
                response.Data = "Usuario Desactivado";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.StatusCode = (int)EnumStatus.Error;
            }

            return Ok(response);
        }

        [HttpPut]
        [ValidateToken]
        [Route("Activate/{usuarioId}")]
        public async Task<ActionResult> ActivateUsuario(string usuarioId)
        {
            ModelResponse<string> response = new(_config.NewToken);
            try
            {
                await _usuarioService.UpdateStatus(usuarioId);
                response.Data = "Usuario Activado";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.StatusCode = (int)EnumStatus.Error;
            }

            return Ok(response);
        }

        [HttpPut]
        [ValidateToken]
        [Route("Update")]
        public async Task<ActionResult> UpdateUsuario(UsuarioConsultaModelView model)
        {
            ModelResponse<string> response = new(_config.NewToken);
            try
            {
                var entity = await _usuarioService.GetUsuarioAndDirectionById(model.UsuarioId);
                await _usuarioService.Update(UsuarioHelper.UsuarioConsultaModelViewToView(model, entity));
                response.Data = "Usuario Activado";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.StatusCode = (int)EnumStatus.Error;
            }

            return Ok(response);
        }

        [HttpGet]
        [ValidateToken]
        [Route("Reporte")]
        public async Task<ActionResult> Reporte()
        {
            ModelResponse<List<UsuarioReporteModelView>> response = new(_config.NewToken);
            try
            {
                Usuario registros = await _usuarioService.GetRegistrosByUser(_config.Usuario.UsuarioId);
                response.Data = UsuarioHelper.EntityToReportModel(registros);
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
