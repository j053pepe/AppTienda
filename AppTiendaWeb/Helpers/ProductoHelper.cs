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
    }
}
