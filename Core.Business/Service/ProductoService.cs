using Core.Contracts.Service;
using Core.Models.AppTiendaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.Service
{
    public class ProductoService : IProductoService
    {
        public Task DeleteProducto(int productoId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Producto>> GetAllProductsByTienda(int tiendaId)
        {
            throw new NotImplementedException();
        }

        public Task<Producto> GetProductoById(int productoId)
        {
            throw new NotImplementedException();
        }

        public Task NuevoProducto(Producto entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProducto(Producto entity)
        {
            throw new NotImplementedException();
        }
    }
}
