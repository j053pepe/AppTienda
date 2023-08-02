using Core.Business;
using Core.Contracts.Service;
using Core.Models;
using Core.Models.AppTiendaModels;
using Core.Models.AppTiendaWebModels;
using Microsoft.AspNetCore.Mvc;
using Presentation.AppTiendaWeb.CustomAttributes;
using Presentation.AppTiendaWeb.Helpers;
using System.Text.Json;

namespace Presentation.AppTiendaWeb.Controllers
{
    [Route("api/tienda")]
    [ApiController]
    public class TiendaController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITiendaService _tiendaService;
        private readonly ConfigAppWeb _config;
        public TiendaController(IUsuarioService usuarioService, ITiendaService tiendaService, ConfigAppWeb config)
        {
            _usuarioService = usuarioService;
            _tiendaService = tiendaService;
            _config = config;
        }

        [HttpPost]
        [ValidateToken]
        [Route("Register")]
        public async Task<ActionResult> NuevaTienda([FromForm] TiendaNewModelView tiendaWeb)
        {
            ModelResponse<string> modelResponse = new(_config.NewToken);
            try
            {
                Tienda entity = TiendaHelper.ViewRegisterToEntity(tiendaWeb, _config.Usuario.UsuarioId);
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

        [HttpPut]
        [ValidateToken]
        [Route("Actualizar/{tiendaId}")]
        public async Task<ActionResult> Actualizar([FromForm] TiendaEditModelView tiendaWeb, int tiendaId)
        {
            ModelResponse<string> modelResponse = new(_config.NewToken);
            try
            {
                Tienda entity = await _tiendaService.GetTiendaById(tiendaId);               

                if (entity != null)
                {
                    entity = TiendaHelper.ModelViewToEntity(tiendaWeb, entity);
                    if (tiendaWeb.ImagenTienda != null)                    
                        entity.TiendaDetalle.UrlImage = TiendaHelper.GuardarImagenTienda(entity.TiendaId, tiendaWeb.ImagenTienda);
                    
                    await _tiendaService.UpdateTienda(entity);
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

        [HttpGet]
        [ValidateToken]
        [Route("BasicData")]
        public async Task<ActionResult> Get()
        {
            ModelResponse<dynamic> modelResponse = new(_config.NewToken);
            try
            {
                Tienda responseTiendaNueva = await _tiendaService.GetTienda();
                modelResponse.Data = TiendaHelper.EntityToDynamic(responseTiendaNueva);
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
