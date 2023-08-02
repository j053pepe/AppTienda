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
                Nombre = tiendaWeb.NombreTiendaNueva,
                UsuarioId = usuarioId,
                TiendaDetalle = new TiendaDetalle
                {
                    Descripcion = tiendaWeb.DescripcionTiendaNueva,
                    Direccion = tiendaWeb.DireccionTiendaNueva,
                    UrlImage = ""
                }
            };
        }

        public static dynamic EntityToDynamic(Tienda tienda)
        {
            return new
            {
                tienda.TiendaId,
                tienda.FechaCreacion,
                tienda.Nombre,
                tienda.UsuarioId,
                tienda.TiendaDetalle.Direccion,
                UrlImage = tienda.TiendaDetalle.UrlImage.Replace("wwwroot", ""),
                tienda.TiendaDetalle.Descripcion
            };
        }

        public static string GuardarImagenTienda(int tiendaId, IFormFile formFile)
        {
            byte[] fileByteArray;
            string pathRoot = @$"wwwroot/Images/Tienda/{tiendaId}";
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
            return pathRoot.Replace("wwwroot", "");
        }

        public static Tienda ModelViewToEntity(TiendaEditModelView tiendaWeb, Tienda entity)
        {
            entity.Nombre = tiendaWeb.NombreTienda;
            entity.TiendaDetalle.Descripcion = tiendaWeb.DescripcionTienda;
            entity.TiendaDetalle.Direccion = tiendaWeb.DireccionTienda;

            return entity;
        }
    }
}
