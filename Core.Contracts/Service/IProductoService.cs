using Core.Models.AppTiendaModels;

namespace Core.Contracts.Service
{
    public interface IProductoService
    {
        public Task Nuevo(Producto entity);
        public Task Update(Producto entity);
        public Task UpdateStatus(int productoId);
        public Task<Producto> GetById(int productoId, string properties);
        public Task<List<Producto>> GetAllProducts(string includeProperties);
        public Task<Producto> GetByCodigo(string codigo);
        public Task<List<Producto>> GetByFilter(string query);
        public Task UpdateStock(int? productoId, decimal? cantidad);
    }
}
