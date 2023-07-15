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
        [Route("")]
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

        [HttpGet]
        [Route("{Query}")]
        public async Task<ActionResult> Get(string Query)
        {
            List<string> ResultFilter = new List<string>();
            List<Producto> products = await _productoService.GetByFilter(Query);
            ResultFilter = products.Select(x => $"{x.Nombre} | {x.Codigo} | " + String.Format("{0:c}", x.Precio.Value)).ToList();

            return Ok(ResultFilter);
        }

        [HttpPost]
        [ValidateToken]
        [Route("")]
        public async Task<ActionResult> Post([FromForm] ProductoModelView model)
        {
            ModelResponse<string> modelResponse = new();
            try
            {
                Producto entityCode = await _productoService.GetByCodigo(model.Codigo);
                if (entityCode == null)
                {
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
                else
                {
                    modelResponse.StatusCode = 500;
                    modelResponse.Message = "Codigo ingresado ya existe.";
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
        [Route("")]
        public async Task<ActionResult> Put([FromForm] ProductoModelView model)
        {
            ModelResponse<string> modelResponse = new();
            try
            {
                Producto entity = await _productoService.GetById(model.ProductId.Value, "ProductoDetalle");
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
        [Route("{productoId}")]
        public async Task<ActionResult> ChangeStatus(int productoId)
        {
            ModelResponse<string> modelResponse = new();
            try
            {
                Producto entity = await _productoService.GetById(productoId, string.Empty);
                if (entity != null)
                {
                    entity.Activo = !entity.Activo;
                    await _productoService.Update(entity);
                }
                else
                {
                    modelResponse.Message = "Producto no encontrado";
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
