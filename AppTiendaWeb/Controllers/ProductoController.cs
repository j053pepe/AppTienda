using Core.Business.Service;
using Core.Contracts.Service;
using Core.Models.AppTiendaModels;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.AppTiendaWeb.Helpers;
using Core.Models.AppTiendaWebModels;
using Presentation.AppTiendaWeb.CustomAttributes;
using Core.Business;

namespace Presentation.AppTiendaWeb.Controllers
{
    [Route("api/Producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        readonly IProductoService _productoService;
        private readonly ConfigAppWeb _config;
        public ProductoController(IProductoService productoService, ConfigAppWeb config)
        {
            _productoService = productoService;
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            ModelResponse<List<ProductoModelView>> modelResponse = new ModelResponse<List<ProductoModelView>>();
            try
            {
                List<Producto> products = await _productoService.GetAllProducts();
                modelResponse.Data = products.Select(x => ProductoHelper.EntityToModelView(x)).ToList();
            }
            catch (Exception ex)
            {
                modelResponse.Message = ex.Message;
                modelResponse.StatusCode = 500;
            }
            return Ok(modelResponse);
        }

        [HttpPost]
        [ValidateToken]
        public async Task<ActionResult> Post([FromForm] ProductoModelView model)
        {
            ModelResponse<string> modelResponse = new();
            try
            {
                // Agregar codigo de buscar codigo de producto _productoService.ExistCodigo();
                Producto entity = ProductoHelper.EntityToModelView(model);
                entity.UsuarioId = _config.Usuario.UsuarioId;
                await _productoService.Nuevo(entity);

                if (entity.ProductoId != null && model.ImagenProducto != null)
                {
                    entity.ProductoDetalle.UrlImage = ProductoHelper.GuardarImagenTienda(entity.ProductoId.Value, model.ImagenProducto);
                    await _productoService.Update(entity);
                }

                modelResponse.Data = "Producto guardado correctamente.";
            }
            catch (Exception ex)
            {
                modelResponse.Message = ex.Message;
                modelResponse.StatusCode = 500;
            }
            return Ok(modelResponse);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromForm] ProductoModelView model)
        {
            ModelResponse<string> modelResponse = new();
            try
            {
                Producto entity = await _productoService.GetById(model.ProductId.Value);
                entity.Precio = model.Precio;
                entity.Codigo = model.Codigo;
                entity.Nombre = model.Nombre;
                entity.Stock = model.Stock;
                entity.ProductoDetalle.Descripcion = model.Descripcion;

                await _productoService.Update(entity);

                if (model.ImagenProducto != null)
                {
                    entity.ProductoDetalle.UrlImage = ProductoHelper.GuardarImagenTienda(entity.ProductoId.Value, model.ImagenProducto);
                    await _productoService.Update(entity);
                }

                modelResponse.Data = "Producto actualizado correctamente.";
            }
            catch (Exception ex)
            {
                modelResponse.Message = ex.Message;
                modelResponse.StatusCode = 500;
            }
            return Ok(modelResponse);
        }

        [HttpPut]
        [Route("UpdateStatus/{productoId}")]
        public async Task<ActionResult> ChangeStatus(int productoId)
        {
            ModelResponse<string> modelResponse = new();
            try
            {

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
