using Core.Contracts.Data;
using Core.Contracts.Repositories;
using Core.Models.AppTiendaModels;

namespace Infraestructure.Data.Repositories
{
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        public ProductoRepository(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
