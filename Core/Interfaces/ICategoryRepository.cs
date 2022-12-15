using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IReadOnlyList<Category>> GetCategoriesAsync();
        Task<bool> SaveChangesAsync();
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}