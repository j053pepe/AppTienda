using Core.Models.AppTiendaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Service
{
    public interface IVentaService
    {
        public Task NuevaVenta(Venta entity);
        public Task UpdateVenta(Venta entity);
        public Task DeleteVenta(int ventaId);
        public Task<Venta> GetVentaById(int ventaId);
        public Task<List<Venta>> GetAllVenta();
        public Task<List<Venta>> GetAllVentasByUsuario(string usuarioId);
    }
}
