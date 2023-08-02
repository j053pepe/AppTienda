using Core.Business.Service;
using Core.Contracts.Service;
using Core.Models.AppTiendaModels;
using Core.Models.AppTiendaWebModels;

namespace Presentation.AppTiendaWeb.Helpers
{
    public class VentaHelper
    {
        public static async Task<Venta> ModelToEntityAsync(IProductoService productoService, VentaModelView model, UsuarioAuthModelView usuario)
        {
            Venta entity = new Venta()
            {
                Activo = true,
                Fecha = DateTime.Now,
                NumeroPoductos = model.VentaDetalle.Count,
                UsuarioId = usuario.UsuarioId
            };

            for (int indice = 0; indice < model.VentaDetalle.Count; indice++)
            {
                VentaDetalleModelView item = model.VentaDetalle[indice];
                Producto productoDb = await productoService.GetByCodigo(item.Codigo.Trim());
                if (productoDb == null)
                {
                    indice = model.VentaDetalle.Count;
                    throw new Exception($"El codigo: ${item.Codigo} del producto no existe");
                }
                else if (!productoDb.Activo.Value)
                {
                    indice = model.VentaDetalle.Count;
                    throw new Exception($"El producto {productoDb.Nombre} fue dado de baja");
                }
                else if (productoDb.Stock < item.Cantidad)
                {
                    indice = model.VentaDetalle.Count;
                    throw new Exception($"La cantidad del producto ${productoDb.Nombre} excede el stock: Solicitado=${item.Cantidad} - Stock=${productoDb.Stock}");
                }

                VentaDetalle detalle = new VentaDetalle()
                {
                    ProductoId = productoDb.ProductoId,
                    Precio = productoDb.Precio,
                    Cantidad = item.Cantidad,
                    Total = item.Cantidad * productoDb.Precio
                };
                entity.VentaDetalle.Add(detalle);
            }

            entity.Total = entity.VentaDetalle.Sum(x => x.Total);

            return entity;
        }

        public static VentaReporteModelView EntityToReportModel(Venta x)
        {
            return new VentaReporteModelView()
            {
                Cantidad = x.NumeroPoductos,
                FechaVenta = x.Fecha.ToString("dd/MM/yyyy h:mm:ss tt"),
                NombreUsuario = $"{x.Usuario.Nombre} {x.Usuario.ApellidoPaterno} {x.Usuario.ApellidoMaterno}",
                Status = x.Activo.Value,
                Total = x.Total,
                VentaId = x.VentaId.Value,
                Productos = x.VentaDetalle.Select(z => new VentaReporteDetalleModelView
                {
                    Cantidad = z.Cantidad,
                    NombreCodigo = $"{z.Producto.Nombre} | {z.Producto.Codigo}",
                    PrecioUnitario = z.Precio,
                    Total = z.Total,
                    VentaDetalleId = z.VentaDetalleId
                }).ToList()
            };
        }
    }
}
