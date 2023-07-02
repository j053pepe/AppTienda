using Core.Contracts.Data;
using Core.Contracts.Repositories;
using Core.Models.AppTiendaModels;

namespace Infraestructure.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
