using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract
{
    public interface IGenericRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey id, params string[] includes);

        Task AddAysnc(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes);
        #region With  Specifications    

        Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> specifications);

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specifications);

        Task<int> CountAsync(ISpecification<TEntity, TKey> specifications);

        #endregion

    }
}
