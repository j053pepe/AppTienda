using Core.Models.AppTiendaModels;
using Core.Models.AppTiendaWebModels;

namespace Presentation.AppTiendaWeb.Helpers
{
    public class ProductoHelper
    {
        public static ProductoModelView EntityToModelView(Producto model)
        {
            return new ProductoModelView
            {
                Codigo = model.Codigo,
                Descripcion = model.ProductoDetalle.Descripcion,
                Nombre = model.Nombre,
                Precio = model.Precio.Value,
                ProductId = model.ProductoId.Value,
                Status = model.Activo.Value,
                Stock = model.Stock.Value,
                UrlImagen = model.ProductoDetalle.UrlImage
            };
        }

        public static Producto EntityToModelView(ProductoModelView model)
        {
            return new Producto
            {
                Activo = model.Status,
                Codigo = model.Codigo,
                Fecha = DateTime.Now,
                Nombre = model.Nombre,
                Precio = model.Precio,
                Stock = model.Stock,                
                ProductoDetalle = new ProductoDetalle
                {
                    Descripcion = model.Descripcion,
                    UrlImage = ""
                }
            };
        }
        public static string GuardarImagenTienda(int productoId, IFormFile formFile)
        {
            byte[] fileByteArray;
            string pathRoot = @$"wwwroot/Images/Producto";
            if (!Directory.Exists(pathRoot))
                Directory.CreateDirectory(pathRoot);

            pathRoot += $"/{productoId}.{formFile.FileName.Split(".").Last()}";
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
    }
}
