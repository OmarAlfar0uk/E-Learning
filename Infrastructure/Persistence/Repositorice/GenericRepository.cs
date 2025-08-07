using Domain.Contract;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Persistence.Repositorice
{
    public class GenericRepository<TEntity, TKey>(E_LearnDbcontext _dbcontext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAysnc(TEntity entity)
         => await _dbcontext.Set<TEntity>().AddAsync(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _dbcontext.Set<TEntity>().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes)
        {
            IQueryable<TEntity> query = _dbcontext.Set<TEntity>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
            => await _dbcontext.Set<TEntity>().FindAsync(id);

        public async Task<TEntity?> GetByIdAsync(TKey id, params string[] includes)
        {
            IQueryable<TEntity> query = _dbcontext.Set<TEntity>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.Id!.Equals(id));
        }

        public void Remove(TEntity entity)
            => _dbcontext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
            => _dbcontext.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbcontext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            IQueryable<TEntity> query = _dbcontext.Set<TEntity>().Where(predicate);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }
        #region With  Specifications    

        #region With  Specifications    

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator.CreateQurery(_dbcontext.Set<TEntity>(), specifications).ToListAsync();
        }


        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity , TKey> specifications)
        {
            return await SpecificationEvaluator.CreateQurery(_dbcontext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecification<TEntity, TKey> specifications)
           => await SpecificationEvaluator.CreateQurery(_dbcontext.Set<TEntity>(), specifications).CountAsync();

        #endregion
        #endregion
    }
}
