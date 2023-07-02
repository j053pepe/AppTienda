using Core.Models;
using Core.Models.AppTiendaWebModels;
using Core.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.AppTiendaWeb.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public ActionResult<ModelResponse<string>> Login(UsuarioLoginModelView user)
        {
            ModelResponse<string> modelResponse = new();
            try
            {

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
