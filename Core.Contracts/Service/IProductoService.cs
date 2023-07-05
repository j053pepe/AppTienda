using Core.Models.AppTiendaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Service
{
    public interface IProductoService
    {
        public Task NuevoProducto(Producto entity);
        public Task UpdateProducto(Producto entity);
        public Task DeleteProducto(int productoId);
        public Task<Producto> GetProductoById(int productoId);
        public Task<List<Producto>> GetAllProductsByTienda(int tiendaId);
    }
}
