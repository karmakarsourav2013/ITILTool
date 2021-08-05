using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region Commands

        void Create(TEntity entity);

        void Create(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        void Delete(int id);

        #endregion

        #region Async Queries

        // Here we use the OData or GraphQL to fetch data 

        #endregion
    }
}
