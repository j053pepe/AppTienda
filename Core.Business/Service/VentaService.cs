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

        public Task NuevaVenta(Venta entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateVenta(Venta entity)
        {
            throw new NotImplementedException();
        }
    }
}
