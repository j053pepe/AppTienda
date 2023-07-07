using Core.Contracts.Service;
using Core.Models;
using Core.Models.AppTiendaModels;
using Core.Models.AppTiendaWebModels;
using Microsoft.AspNetCore.Mvc;
using Presentation.AppTiendaWeb.CustomAttributes;
using Presentation.AppTiendaWeb.Helpers;

namespace Presentation.AppTiendaWeb.Controllers
{
    [Route("api/tienda")]
    [ApiController]
    public class TiendaController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITiendaService _tiendaService;
        public TiendaController(IUsuarioService usuarioService, ITiendaService tiendaService)
        {
            _usuarioService = usuarioService;
            _tiendaService = tiendaService;
        }

        [HttpPost]
        [ValidateToken]
        [Route("Register")]        
        public async Task<ActionResult<ModelResponse<string>>> NuevaTienda([FromForm]TiendaNewModelView tiendaWeb)
        {
            ModelResponse<string> modelResponse = new ModelResponse<string>();
            try
            {
                string token = Request.Headers["Token"];
                var responseUser = await UsuarioHelper.TokenToUsuarioAsync(token, _usuarioService);
                Tienda entity = TiendaHelper.ViewRegisterToEntity(tiendaWeb, responseUser.UsuarioId);
                await _tiendaService.NuevaTienda(entity);
                Tienda responseTiendaNueva = await _tiendaService.GetTienda();
                if (responseTiendaNueva != null)
                {
                    responseTiendaNueva.TiendaDetalle.UrlImage = TiendaHelper.GuardarImagenTienda(responseTiendaNueva.TiendaId, tiendaWeb.ImagenTienda);
                    await _tiendaService.UpdateTienda(responseTiendaNueva);
                    modelResponse.Data = "Se genero la tienda correctamente";
                }
                else
                {
                    modelResponse.Message = "Error al crear la tienda";
                    modelResponse.StatusCode = 500;
                }
            }
            catch (Exception ex)
            {
                modelResponse.Message = ex.Message;
                modelResponse.StatusCode = 500;
            }
            return Ok(modelResponse);
        }
    }
}
