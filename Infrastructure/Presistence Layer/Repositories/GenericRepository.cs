using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Presistence_Layer.Data;


namespace Presistence_Layer.Repositories
{
    internal class GenericRepository<TEntity, Tkey>(StoreDbContext _storeContext): IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public async Task AddAsync(TEntity entity)
        {
            await _storeContext.Set<TEntity>().AddAsync(entity);
        }

        public void Remove(TEntity entity)
        {
             _storeContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            #region Specification definition
            //  The Specification Pattern is used to separate filtering logic from the repository or service.
            //  Instead of writing conditions directly in GetAll you create reusable  Specifications that describe filtering rules.
            // we can return condition base on some parameters for example we can filter products by product name
            // or we can include related entities like product brand and product type
            // so we can use specification pattern to achieve (conditions and includes or paginations or....) 
            #endregion

            #region Old way  to use specifications 

            //public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? condition = default, List<Expression<Func<TEntity, object>>>? includes = default)
            //{ 
            //if (condition is not null)
            //{
            //    return await _storeContext.Set<TEntity>().Where(condition).ToListAsync();
            //}
            //if (includes is not null)
            //{
            //    IQueryable<TEntity> EntryPoint = _storeContext.Set<TEntity>();
            //    foreach (var include in includes)
            //    {
            //        EntryPoint = EntryPoint.Include(include);
            //    }
            //    return await EntryPoint.ToListAsync();
            //}
            //else
            //{
            //    return await _storeContext.Set<TEntity>().ToListAsync();
            //}
            //if we want to add another specifications like pagination we can add it here with else if and so on and its not a good way to do that
            // } 
            #endregion
            return await _storeContext.Set<TEntity>().ToListAsync();
        }
       

        public async Task<TEntity?> GetByIdAsync(Tkey id)  
         => await _storeContext.Set<TEntity>().FindAsync(id);
        

        public void Update(TEntity entity)
        {
            _storeContext.Set<TEntity>().Update(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey> specification)
        {
            var query = SpecificationEvaluator.CreateQuery(_storeContext.Set<TEntity>(), specification);
            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, Tkey> specification)
        {
            var query = SpecificationEvaluator.CreateQuery(_storeContext.Set<TEntity>(), specification).FirstOrDefaultAsync();
            return await query;
        }

        public async Task<int> CountAsync(ISpecification<TEntity, Tkey> specification)
        {
            return await SpecificationEvaluator.CreateQuery(_storeContext.Set<TEntity>(), specification).CountAsync();
        }
    }
}
