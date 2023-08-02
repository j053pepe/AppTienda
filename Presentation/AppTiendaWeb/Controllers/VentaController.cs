using Core.Business;
using Core.Business.Service;
using Core.Contracts.Service;
using Core.Models.AppTiendaModels;
using Core.Models;
using Core.Models.AppTiendaWebModels;
using Microsoft.AspNetCore.Mvc;
using Presentation.AppTiendaWeb.CustomAttributes;
using Presentation.AppTiendaWeb.Helpers;

namespace Presentation.AppTiendaWeb.Controllers
{
    [Route("api/Venta")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        readonly IVentaService _ventaService;
        readonly IProductoService _productoService;
        private readonly ConfigAppWeb _config;
        public VentaController(IVentaService ventaService, ConfigAppWeb config, IProductoService productoService)
        {
            _ventaService = ventaService;
            _config = config;
            _productoService = productoService;
        }

        [HttpPost]
        [ValidateToken]
        [Route("")]
        public async Task<ActionResult> Nueva(VentaModelView model)
        {
            ModelResponse<string> modelResponse = new(_config.NewToken);
            try
            {
                Venta entity = await VentaHelper.ModelToEntityAsync(_productoService, model, _config.Usuario);
                await _ventaService.NuevaVenta(entity);
                if (entity.VentaId != null)
                {
                    foreach (VentaDetalle detalle in entity.VentaDetalle)
                    {
                        await _productoService.UpdateStock(detalle.ProductoId, detalle.Cantidad);
                    }
                }

                modelResponse.Data = "Venta generada correctamente";
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
        [Route("Reporte")]
        public async Task<ActionResult> GetReporte()
        {
            ModelResponse<List<VentaReporteModelView>> modelResponse = new(_config.NewToken);
            try
            {
                List<Venta> Ventas = await _ventaService.GetAllVenta();
                if (Ventas.Any())
                {
                    modelResponse.Data = Ventas.Select(x => VentaHelper.EntityToReportModel(x)).ToList();
                }
            }
            catch (Exception ex)
            {
                modelResponse.Message = ex.Message;
                modelResponse.StatusCode = 500;
            }
            return Ok(modelResponse);
        }

        [HttpDelete]
        [ValidateToken]
        [Route("{ventaId}")]
        public async Task<ActionResult> UpdateStatus(int ventaId)
        {
            ModelResponse<string> modelResponse = new(_config.NewToken);
            try
            {
                Venta entity = await _ventaService.GetVentaById(ventaId);
                entity.Activo = !entity.Activo;
                await _ventaService.UpdateVenta(entity);
                modelResponse.Data = "Venta actualizada correctamente";
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
