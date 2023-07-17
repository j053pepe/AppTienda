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
    public class VentaService : IVentaService
    {
        private IVentaRepository _ventaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VentaService(IVentaRepository ventaRepository, IUnitOfWork unitOfWork)
        {
            _ventaRepository = ventaRepository;
            _unitOfWork = unitOfWork;
        }
        public Task DeleteVenta(int ventaId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Venta>> GetAllVentaByTienda(int tiendaId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Venta>> GetAllVentasByUsuario(string usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<Venta> GetVentaById(int ventaId)
        {
            throw new NotImplementedException();
        }

        public async Task NuevaVenta(Venta entity)
        {
            await _ventaRepository.Insert(entity);
            await _unitOfWork.SaveAsync();
        }

        public Task UpdateVenta(Venta entity)
        {
            throw new NotImplementedException();
        }
    }
}
