using Core.Business;
using Core.Contracts.Service;
using Core.Models;
using Core.Models.AppTiendaModels;
using Core.Models.AppTiendaWebModels;
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
            ModelResponse<string> modelResponse = new("");
            modelResponse.StatusCode = (int)EnumStatus.Error;
            modelResponse.Message = "Token Invalido.";

            string token = context.HttpContext.Request.Headers["Token"];
            if (token == null)
                context.Result = new OkObjectResult(modelResponse);
            else
            {
                var response = UsuarioHelper.TokenToUsuario(token);
                if (response is null)
                    context.Result = new OkObjectResult(modelResponse);
                else
                {
                    if (DateTime.Now > response.UsuarioTime.AddMinutes(30))
                    {
                        modelResponse.Message = "Token expirado.";
                        context.Result = new OkObjectResult(modelResponse);
                    }
                    else
                    {
                        ConfigAppWeb _config = context.HttpContext.RequestServices.GetRequiredService<ConfigAppWeb>();
                        _config = RefreshToken(response, _config);
                        await next();
                    }
                }
            }
        }

        private ConfigAppWeb RefreshToken(UsuarioAuthModelView response, ConfigAppWeb config)
        {
            DateTime nowDate = DateTime.Now;
            DateTime timeToken30 = response.UsuarioTime.AddMinutes(30);
            TimeSpan diff;
            diff = timeToken30 - nowDate;

             if(diff.Minutes>0 && diff.Minutes <= 5)
            {
                response.UsuarioTime = response.UsuarioTime.AddMinutes(30);
                config.NewToken = AesOperationHelper.EncryptString(response.ToJsonString());
                
            }
            config.Usuario = response;
            return config;
        }
    }
}
