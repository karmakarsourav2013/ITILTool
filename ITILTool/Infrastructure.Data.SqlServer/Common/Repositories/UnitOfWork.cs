using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.SqlServer.Common.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DataContext
    {
        private bool _disposed;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
                _repositories[type] = new Repository<TEntity>(Context);

            return (IRepository<TEntity>)_repositories[type];
        }

        public TContext Context { get; }

        public int SaveChanges()
        {
            AuditEntities();

            return Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AuditEntities();

            return await Context.SaveChangesAsync();
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context?.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private void AuditEntities()
        {
            DateTime now = DateTime.UtcNow;

            var entries = Context.ChangeTracker.Entries()
                .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity.GetType().GetProperty("CreatedDate") != null)
                        entry.Property("CreatedDate").CurrentValue = now;
                }

                if (entry.Entity.GetType().GetProperty("ModifiedDate") != null)
                    entry.Property("ModifiedDate").CurrentValue = now;
            }
        }
    }
}
