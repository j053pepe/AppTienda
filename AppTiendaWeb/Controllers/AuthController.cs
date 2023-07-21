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
        [Route("Login")]
        public async Task<ActionResult<ModelResponse<string>>> Login(UsuarioLoginModelView user)
        {
            ModelResponse<string> modelResponse = new("");
            try
            {
                var userDb = await _usuarioService.GetUsuarioByEmail(user.EmailLogin);
                if (userDb == null)
                {
                    modelResponse.Message = "email o contraseña incorrecta.";
                    modelResponse.StatusCode = (int)EnumStatus.CredencialesIncorrectas;
                }
                else
                {

                    if (!UsuarioHelper.VerifyHashedPassword(userDb.Password, user.PasswordLogin))
                    {
                        modelResponse.Message = "email o contraseña incorrecta.";
                        modelResponse.StatusCode = (int)EnumStatus.CredencialesIncorrectas;
                    }
                    else
                    {
                        UsuarioAuthModelView model = new(userDb);
                        modelResponse.Message = "Login correcto";
                        modelResponse.Data = $"{AesOperationHelper.EncryptString(model.ToJsonString())}";
                    }
                }
            }
            catch (Exception ex)
            {
                modelResponse.Message = ex.Message;
                modelResponse.StatusCode = (int)EnumStatus.CredencialesIncorrectas;
            }

            return Ok(modelResponse);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<ModelResponse<string>>> Register([FromForm] UsuarioNewModelView newUser)
        {
            ModelResponse<string> modelResponse = new("");
            try
            {
                var usuarioFind = await _usuarioService.GetUsuarioByEmail(newUser.Email);
                if (usuarioFind != null)
                {
                    modelResponse.StatusCode = (int)EnumStatus.Error;
                    modelResponse.Message = "Error al registrar usuario.";
                    modelResponse.Data = "El correo ya existe en la base de datos.";
                }
                else
                {
                    Usuario entity = UsuarioHelper.UsuarioModelViewToModelDb(newUser);
                    entity.Activo = newUser.Activo;
                    entity.Password = UsuarioHelper.HashPassword(newUser.Password);
                    await _usuarioService.Create(entity);
                    modelResponse.Data = "Registro creado con exito.";
                }
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
            ModelResponse<bool> modelResponse = new("");
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
