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
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductoService(IProductoRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task UpdateStatus(int productoId)
        {
            var product = await _repository.GetByIdAsync(productoId);
            product.Activo = !product.Activo;
            await _repository.Update(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<Producto>> GetAllProducts()
        {
            var list =  await _repository.Get(filter: null, orderBy: null, includeProperties: "ProductoDetalle");
            return list.ToList();
        }

        public async Task<Producto> GetById(int productoId)
        {
            return await _repository.GetByIdAsync(productoId);
        }

        public async Task Nuevo(Producto entity)
        {
            await _repository.Insert(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(Producto entity)
        {
            await _repository.Update(entity);
            await _unitOfWork.SaveAsync();
        }
    }
}
