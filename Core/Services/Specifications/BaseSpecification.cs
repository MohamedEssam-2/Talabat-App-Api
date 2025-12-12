using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entitys;

namespace Services_Layer.Specifications
{
    internal abstract class BaseSpecification<TEntity, Tkey> : ISpecification<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        #region Include
        public ICollection<Expression<Func<TEntity, object>>> Include_Expressions { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> _includeExpression)
        {
            Include_Expressions.Add(_includeExpression);
        } 
        #endregion

        #region Criteria
        public Expression<Func<TEntity, bool>> Criteria { get; }

        protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
        #endregion

        #region Order
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending  { get; private set; }
   

        public void AddOrderByAscending(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public void AddOrderByDescending(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderByDescending = orderByExpression;
        }
        #endregion

        #region Pagination
        public int Take { get ; private set; }
        public int Skip { get; private set; }
        public bool IsPaginated { get; private set; }
        protected void ApplyPagination(int PageSize, int PageIndex)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex-1)*PageSize;
            
        }
        #endregion
    }
}
