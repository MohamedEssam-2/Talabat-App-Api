using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Presistence_Layer
{
    public static class SpecificationEvaluator 
    {
        //This class is responsible for applying the specifications to the queries
        // create dynamic queries based on the specifications provided
       
        public static IQueryable<TEntity> CreateQuery<TEntity,Tkey>(IQueryable<TEntity> EntryPoint ,ISpecification<TEntity, Tkey> specification) where TEntity : BaseEntity<Tkey>
        {
            var query = EntryPoint;//dbContext.Set<TEntity>()
            if (specification is not null)
                if (specification.Include_Expressions is not null)
                {
                    if (specification.Criteria is not null)
                    {
                        query = query.Where(specification.Criteria);//filter before inludes 
                    }
                    foreach (var includeExpression in specification.Include_Expressions)
                    {
                        query = query.Include(includeExpression);//Mange includes(Related-data(Eager-Loading))
                    }
                    if(specification.OrderBy is not null)
                    {
                        query = query.OrderBy(specification.OrderBy);
                    }
                    if (specification.OrderByDescending is not null)
                    {
                        query = query.OrderByDescending(specification.OrderByDescending);
                    }
                    if (specification.IsPaginated)
                    {
                        query = query.Skip(specification.Skip).Take(specification.Take);
                    }
                }
            return query;
        }
    }
}
