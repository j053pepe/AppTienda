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

        public async Task<Producto> GetById(int productoId, string properties)
        {
            var list = await _repository.Get(x=> x.ProductoId==productoId, orderBy: null, includeProperties: properties);
            return list.FirstOrDefault();
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

        public async Task<Producto> GetByCodigo(string codigo)
        {
            var list = await _repository.Get(x => x.Codigo == codigo, orderBy: null, includeProperties: "ProductoDetalle");
            return list.FirstOrDefault();
        }

        public async Task<List<Producto>> GetByFilter(string query)
        {
            var list = await _repository.Get(x => x.Activo.Value && (x.Codigo.ToLower().Contains(query.ToLower()) || x.Nombre.ToLower().Contains(query.ToLower())));
            return list.ToList();
        }

        public async Task UpdateStock(int? productoId, decimal? cantidad)
        {
            Producto entity = await _repository.GetByIdAsync(productoId);
            entity.Stock += cantidad.Value;
            await _repository.Update(entity);
            await _unitOfWork.SaveAsync();
        }
    }
}
