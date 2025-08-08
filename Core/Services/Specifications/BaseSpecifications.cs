using Domain.Contract;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    abstract class BaseSpecifications<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }

        public Expression<Func<TEntity, bool>> Criteria { get; private set; }


        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];



        protected void AddInclode(Expression<Func<TEntity, object>> includeExpressions)
         => IncludeExpressions.Add(includeExpressions);

        #region Sortion
        public Expression<Func<TEntity, object>> OrderBy  { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByExp) => OrderBy = OrderByExp;
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> OrderByDescExp) => OrderByDescending = OrderByDescExp;
        #endregion

        #region Pagination
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get;  set; }
        protected void ApplyPagination(int PageSize, int PageIndex)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;
        }
        #endregion
    }
}
