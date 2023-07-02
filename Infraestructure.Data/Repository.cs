﻿using Core.Contracts.Data;
using Infraestructure.Data.StoreDbMapping;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infraestructure.Data
{

    public partial class Repository<T> : IRepository<T> where T : class
    {
        #region Fields

        internal AppTiendaContext _context;
        internal DbSet<T> _entities;

        #endregion Fields

        #region Constructor

        public Repository(IUnitOfWork uow)
        {
            _context = (AppTiendaContext)uow.Context;
        }

        #endregion Constructor

        #region Properties

        public virtual IQueryable<T> Table => Entities;

        public virtual IQueryable<T> TableNoTracking => Entities.AsNoTracking();

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<T>();
                }
                return _entities;
            }
        }

        #endregion Properties

        #region Methods

        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await Entities.FindAsync(id);
        }

        public virtual async Task Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Entities.Add(entity);
            await Task.CompletedTask;
        }

        public virtual async Task Insert(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (T entity in entities)
            {
                await Insert(entity);
            }
        }

        public virtual async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task Update(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (T item in entities)
            {
                await Update(item);
            }
        }

        public virtual async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Entry(entity).State = EntityState.Deleted;

            await Task.CompletedTask;
        }

        public virtual async Task Delete(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (T entity in entities)
            {
                await Delete(entity);
            }
        }

        public virtual async Task<IEnumerable<T>> Get(
                Expression<Func<T, bool>> filter = null,
                Func<IQueryable<T>, Task<IOrderedQueryable<T>>> orderBy = null,
                string includeProperties = "")
        {
            IQueryable<T> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (string includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public virtual IEnumerable<T> GetPagedElements<S>(int pageIndex, int pageCount,
            Expression<Func<T, S>> orderByExpression, bool ascending,
            Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            //Verificar los argumentos para esta consulta
            if (pageIndex < 0)
            {
                throw new ArgumentException(
                    //Resources.Messages.exception_InvalidPageIndex,
                    "pageIndex");
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(
                    //Resources.Messages.exception_InvalidPageCount,
                    "pageCount");
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression)
                        //, Resources.Messages.exception_OrderByExpressionCannotBeNull
                        );
            }

            IQueryable<T> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (string includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return ascending
                            ?
                        query.OrderBy(orderByExpression)
                            .Skip((pageIndex - 1) * pageCount)
                            .Take(pageCount)
                            :
                        query.OrderByDescending(orderByExpression)
                            .Skip((pageIndex - 1) * pageCount)
                            .Take(pageCount);
        }

        public virtual async Task CancelChanges(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Entry(entity).State = EntityState.Unchanged;
            await Task.CompletedTask;
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

        #endregion Methods
    }
}