using Core.Business.Service;
using Core.Contracts.Service;
using Core.Models.AppTiendaModels;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.AppTiendaWeb.Helpers;
using Core.Models.AppTiendaWebModels;

namespace Presentation.AppTiendaWeb.Controllers
{
    [Route("api/Producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        readonly IProductoService _productoService;
        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
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
        public async Task<ActionResult> Post([FromForm] ProductoModelView model)
        {
            ModelResponse<string> modelResponse = new ();
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

        [HttpPut]
        public async Task<ActionResult> Put([FromForm] ProductoModelView model)
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
