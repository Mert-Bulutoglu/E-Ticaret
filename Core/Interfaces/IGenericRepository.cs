using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<bool> SaveChangesAsync();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        
    }
}