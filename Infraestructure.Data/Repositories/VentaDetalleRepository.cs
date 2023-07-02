using Core.Contracts.Data;
using Core.Contracts.Repositories;
using Core.Models.AppTiendaModels;

namespace Infraestructure.Data.Repositories
{
    public class VentaDetalleRepository : Repository<VentaDetalle>, IVentaDetalleRepository
    {
        public VentaDetalleRepository(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
