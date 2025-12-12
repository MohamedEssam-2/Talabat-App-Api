using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entitys;

namespace Domain.Contracts
{
    public interface IGenericRepository <TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<IEnumerable<TEntity>>GetAllAsync();
        Task<IEnumerable<TEntity>>GetAllAsync(ISpecification<TEntity, Tkey> specification);//new method with specification pattern
        Task<TEntity?> GetByIdAsync(Tkey id);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity, Tkey> specification);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity); //Dont have Update async method in implementation so we use void here
        void Remove(TEntity entity);//Dont have removed async method in implementation so we use void here
        Task<int> CountAsync(ISpecification<TEntity, Tkey> specification);
    }
}
