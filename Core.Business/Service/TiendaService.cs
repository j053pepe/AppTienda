﻿using Core.Contracts.Data;
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
            IEnumerable<Tienda> result = new List<Tienda>();
            result = await _tienda.Get(filter: null, orderBy: null, includeProperties: "TiendaDetalle");
            return result.FirstOrDefault();
        }

        public async Task<Tienda> GetTiendaById(int id)
        {
            var result = await _tienda.Get(x => x.TiendaId == id, orderBy: null, includeProperties: "TiendaDetalle");
            return result.FirstOrDefault();
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
