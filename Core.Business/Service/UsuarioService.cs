using Core.Contracts.Data;
using Core.Contracts.Repositories;
using Core.Contracts.Service;
using Core.Models.AppTiendaModels;

namespace Core.Business.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UsuarioService(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CheckUsersActive()
        {
            var result = await _usuarioRepository.Get(x => x.Activo.Value);
            if (result.Any())
                return true;
            return false;
        }

        public async Task Create(Usuario entity)
        {
            entity.UsuarioId = Guid.NewGuid().ToString();
            await _usuarioRepository.Insert(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<Usuario>> GetAll()
        {
            var result = await _usuarioRepository.Get(filter: null, orderBy: null, includeProperties: "UsuarioDireccion");
            return result.ToList();
        }

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            var result = await _usuarioRepository.Get(x => x.Email == email);
            return result.FirstOrDefault();
        }

        public async Task<Usuario> GetUsuarioById(string usuarioId)
        {
            var result =await _usuarioRepository.GetByIdAsync(usuarioId);
            return result;
        }

        public async Task<bool> Update(Usuario entity)
        {
            await _usuarioRepository.Update(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }        
    }
}
