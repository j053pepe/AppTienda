using Core.Contracts.Service;
using Core.Models;
using Core.Models.AppTiendaModels;
using Core.Models.AppTiendaWebModels;
using Core.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.AppTiendaWeb.Helpers;

namespace Presentation.AppTiendaWeb.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<ActionResult<ModelResponse<string>>> Login(UsuarioLoginModelView user)
        {
            ModelResponse<string> modelResponse = new();
            try
            {
                var userDb = await _usuarioService.GetUsuarioByEmail(user.EmailLogin);
                if (userDb==null)                
                    return NotFound();

                if (!UsuarioHelper.VerifyHashedPassword(userDb.Password, user.PasswordLogin))
                    return BadRequest("email o contraseña incorrecta.");

                modelResponse.Data = "Login correcto";
            }
            catch (Exception ex)
            {
                modelResponse.Message = ex.Message;
                modelResponse.StatusCode = (int)EnumStatus.Error;
            }

            return Ok(modelResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ModelResponse<string>>> Register(UsuarioNewModelView newUser)
        {
            ModelResponse<string> modelResponse = new();
            try
            {
                Usuario entity = UsuarioHelper.UsuarioModelViewToModelDb(newUser);
                entity.Activo = newUser.Activo;
                entity.Password= UsuarioHelper.HashPassword(newUser.Password);
                int result = await _usuarioService.Create(entity);
                modelResponse.Data = "Registro creado con exito.";
            }
            catch (Exception ex)
            {
                modelResponse.Message = ex.Message;
                modelResponse.StatusCode = (int)EnumStatus.Error;
            }

            return Ok(modelResponse);
        }

        [HttpGet]
        [Route("Status")]
        public async Task<ActionResult<string>> Status()
        {
            ModelResponse<bool> modelResponse = new();
            try
            {
                bool result = await _usuarioService.CheckUsersActive();
                modelResponse.Data = result;
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
