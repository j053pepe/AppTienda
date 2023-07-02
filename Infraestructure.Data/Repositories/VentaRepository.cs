using Core.Contracts.Data;
using Core.Contracts.Repositories;
using Core.Models.AppTiendaModels;

namespace Infraestructure.Data.Repositories
{
    public class VentaRepository : Repository<Venta>, IVentaRepository
    {
        public VentaRepository(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
