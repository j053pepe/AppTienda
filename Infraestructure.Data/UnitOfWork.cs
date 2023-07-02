using Core.Contracts.Data;
using Infraestructure.Data.StoreDbMapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly AppTiendaContext _context;

        public UnitOfWork(AppTiendaContext context)
        {
            _context = context;
        }

        public DbContext Context => _context;

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                _context.SaveChanges();
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error saving to db");
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving to db");
            }
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}