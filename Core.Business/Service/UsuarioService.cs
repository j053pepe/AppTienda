using Core.Contracts.Service;
using Core.Models.AppTiendaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.Service
{
    public class UsuarioService : IUsuarioService
    {
        public Task<bool> CheckUsersActive()
        {
            throw new NotImplementedException();
        }

        public Task<int> Create(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Usuario>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetUsuarioByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetUsuarioById(string usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Usuario entity)
        {
            throw new NotImplementedException();
        }
    }
}
