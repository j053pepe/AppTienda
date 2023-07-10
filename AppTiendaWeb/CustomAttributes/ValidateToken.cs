using Core.Business;
using Core.Contracts.Service;
using Core.Models;
using Core.Models.AppTiendaModels;
using Core.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Presentation.AppTiendaWeb.Helpers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Presentation.AppTiendaWeb.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateToken : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ModelResponse<string> modelResponse = new();
            modelResponse.StatusCode = (int)EnumStatus.Error;
            modelResponse.Message = "Token Invalido.";

            string token = context.HttpContext.Request.Headers["Token"];
            if (token == null)
                context.Result = new OkObjectResult(modelResponse);
            else
            {
                IUsuarioService usuarioService = context.HttpContext.RequestServices.GetRequiredService<IUsuarioService>();
                var response = await UsuarioHelper.TokenToUsuarioAsync(token, usuarioService);
                if (response is null)
                    context.Result = new OkObjectResult(modelResponse);
                else
                {
                    if (!response.Activo.Value)
                    {
                        modelResponse.Message = "Usuario inactivo";
                        context.Result = new OkObjectResult(modelResponse);
                    }
                    else
                    {
                        ConfigAppWeb _config = context.HttpContext.RequestServices.GetRequiredService<ConfigAppWeb>();
                        _config.Usuario = response;
                        await next();
                    }
                }
            }
        }
    }
}
