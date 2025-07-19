using Domain.Contract;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistence.Repositorice
{
    public class GenericRepository<TEntity, TKey>(E_LearnDbcontext _dbcontext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAysnc(TEntity entity)
        {
         await  _dbcontext.Set<TEntity>().AddAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbcontext.Set<TEntity>().ToListAsync();


        public async Task<TEntity?> GetByIdAsync(TKey id) =>await _dbcontext.Set<TEntity>().FindAsync(id);

        public void Remove(TEntity entity) =>_dbcontext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) => _dbcontext.Set<TEntity>().Update(entity);
    }
}
