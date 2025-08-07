using Domain.Contract;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositorice
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQurery<TEntity, TKey>(IQueryable<TEntity> InputQuery, ISpecification<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = InputQuery;
          

            if (specifications.OrderBy is not null)
            {
                Query.OrderBy(specifications.OrderBy);
            }

            if (specifications.OrderByDescending is not null)
            {
                Query.OrderByDescending(specifications.OrderByDescending);
            }


            if (specifications.IsPaginated)
            {
                Query = Query.Skip(specifications.Skip).Take(specifications.Take);
            }
            return Query;
        }
    }
}
