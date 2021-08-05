using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.SqlServer.Common.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext AppDbContext;
        protected readonly DbSet<TEntity> AppDbSet;

        public Repository(DataContext context)
        {
            AppDbContext = context ?? throw new ArgumentException(nameof(context));
            AppDbSet = AppDbContext.Set<TEntity>();
        }

        #region Commands

        public virtual void Create(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            AppDbSet.Add(entity);
        }

        public virtual void Create(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            AppDbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            AppDbSet.Update(entity);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            AppDbSet.UpdateRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            AppDbSet.Remove(entity);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            AppDbSet.RemoveRange(entities);
        }

        public virtual void Delete(int id)
        {
            var entity = AppDbSet.Find(id);

            if (entity != null)
                Delete(entity);
        }

        #endregion

        #region Async Queries

        // Here we use the OData or GraphQL to fetch data 

        #endregion
    }
}
