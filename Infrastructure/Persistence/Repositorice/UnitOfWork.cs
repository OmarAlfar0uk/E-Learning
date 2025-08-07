using Domain.Contract;
using Domain.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositorice
{
    public class UnitOfWork(E_LearnDbcontext _dbcontext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
           // if(_repositories.ContainsKey(typeName) )


            if(_repositories.TryGetValue(typeName ,out object? value) )
                return (IGenericRepository<TEntity ,  TKey>)value;
            else
            {
                var Repo = new GenericRepository<TEntity,TKey>(_dbcontext);
                _repositories[typeName] = Repo;
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync() => await _dbcontext.SaveChangesAsync();
    }
}
