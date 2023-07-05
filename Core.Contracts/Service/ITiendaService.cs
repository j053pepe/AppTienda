using Core.Models.AppTiendaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Service
{
    public interface ITiendaService
    {
        public Task<Tienda> GetTienda();
        public Task<Tienda> GetTiendaById(int id);
        public Task NuevaTienda(Tienda entity);
        public Task UpdateTienda(Tienda entity);
    }
}
