﻿using Core.Models.AppTiendaModels;

namespace Core.Contracts.Service
{
    public interface IUsuarioService
    {
        public Task Create(Usuario entity);
        public Task<Usuario> GetUsuarioByEmail(string email);
        public Task<Usuario> GetUsuarioById(string usuarioId);
        public Task<List<Usuario>> GetAll();
        public Task<bool> Update(Usuario entity);
        public Task<bool> CheckUsersActive();
    }
}
