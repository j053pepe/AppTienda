using Core.Models.AppTiendaModels;
using Core.Models.AppTiendaWebModels;

namespace Presentation.AppTiendaWeb.Helpers
{
    public class TiendaHelper
    {
        public static Tienda ViewRegisterToEntity(TiendaNewModelView tiendaWeb, string usuarioId)
        {
            return new Tienda()
            {
                FechaCreacion = DateTime.Now,
                Nombre = tiendaWeb.Nombre,
                UsuarioId = usuarioId,
                TiendaDetalle = new TiendaDetalle
                {
                    Descripcion = tiendaWeb.Descripcion,
                    Direccion = tiendaWeb.Direccion,
                    UrlImage = ""
                }
            };
        }

        public static string GuardarImagenTienda(int tiendaId, IFormFile formFile)
        {
            byte[] fileByteArray;
            string pathRoot = @$"~wwwroot/Images/Tienda/{tiendaId}";
            if (!Directory.Exists(pathRoot))
                Directory.CreateDirectory(pathRoot);

            pathRoot += $"/{formFile.FileName}";
            if (File.Exists(pathRoot))
                File.Delete(pathRoot);
            else
            {
                using (var item = new MemoryStream())
                {
                    formFile.CopyTo(item);
                    fileByteArray = item.ToArray(); //2nd change here
                    File.WriteAllBytes(pathRoot, fileByteArray);
                }
            }
            return pathRoot;
        }
    }
}
