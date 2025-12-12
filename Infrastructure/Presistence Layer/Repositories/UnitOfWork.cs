using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presistence_Layer.Data;

namespace Presistence_Layer.Repositories
{
    public class UnitOfWork(StoreDbContext _dbcontext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            //Store the repositories in a dictionary to avoid creating multiple instances
            //dictionary => <key,value>
            //key => typeof(TEntity).Name
            //value => object of GenericRepository<TEntity, TKey>
            
            var TypeName = typeof(TEntity).Name;
            if(_repositories.ContainsKey(TypeName))
            {
                return (IGenericRepository<TEntity, TKey>)_repositories[TypeName];
            }
            else

            {
                var repository = new GenericRepository<TEntity, TKey>(_dbcontext);
                _repositories.Add(TypeName, repository);
                return repository;
            }


        }

        public Task<int> SaveChangesAsync()
        {
            return _dbcontext.SaveChangesAsync();
        }
    }
}
