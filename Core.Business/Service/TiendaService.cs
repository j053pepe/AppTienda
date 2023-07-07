using Core.Contracts.Data;
using Core.Contracts.Repositories;
using Core.Contracts.Service;
using Core.Models.AppTiendaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.Service
{
    public class TiendaService : ITiendaService
    {
        private readonly ITiendaRepository _tienda;
        private readonly IUnitOfWork _unitOfWork;
        public TiendaService(ITiendaRepository tienda, IUnitOfWork unitOfWork)
        {
            _tienda = tienda;
            _unitOfWork = unitOfWork;
        }
        public async Task<Tienda> GetTienda()
        {
            IEnumerable<Tienda> result = await _tienda.Get();
            return result.FirstOrDefault();
        }

        public async Task<Tienda> GetTiendaById(int id)
        {
            return await _tienda.GetByIdAsync(id);
        }

        public async Task NuevaTienda(Tienda entity)
        {
            await _tienda.Insert(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateTienda(Tienda entity)
        {
            await _tienda.Update(entity);
            await _unitOfWork.SaveAsync();
        }
    }
}
