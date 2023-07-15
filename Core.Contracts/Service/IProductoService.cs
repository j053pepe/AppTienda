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
        public Task Nuevo(Producto entity);
        public Task Update(Producto entity);
        public Task UpdateStatus(int productoId);
        public Task<Producto> GetById(int productoId, string properties);
        public Task<List<Producto>> GetAllProducts();
        public Task<Producto> GetByCodigo(string codigo);
        public Task<List<Producto>> GetByFilter(string query);
    }
}
