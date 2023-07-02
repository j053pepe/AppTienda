using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Contracts.Data
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }

        void Save();

        Task SaveAsync();

        Task SaveAsync(CancellationToken cancellationToken);
    }
}
