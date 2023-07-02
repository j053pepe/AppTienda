using Core.Contracts.Data;
using Core.Contracts.Repositories;
using Core.Models.AppTiendaModels;

namespace Infraestructure.Data.Repositories
{
    public class TiendaRepository : Repository<Tienda>, ITiendaRepository
    {
        public TiendaRepository(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
